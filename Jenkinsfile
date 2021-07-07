properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('hello') {
            steps {
                // Get some code from a GitHub repository
                echo 'hello from identity'
            }
        }
        stage('deleteWorkspace') {
            steps {
                deleteDir()
            }
        }
        stage('clone') {
            steps {
                // Get some code from a GitHub repository
                git branch: 'master',
                url: 'https://github.com/ProductivityTools-Tasks3/ProductivityTools.GetTask3.Server.git'
            }
        }
		stage('workplacePath'){
			steps{
				echo "${env.WORKSPACE}"
			}
		}
        stage('build') {
            steps {
                bat(script: "dotnet publish ProductivityTools.GetTask3.Server.sln -c Release", returnStdout: true)
            }
        }
        stage('stopMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:GetTask3')
            }
        }

        stage('deleteIisDir') {
            steps {
                retry(5) {
                    bat('if exist "C:\\Bin\\GetTask3" RMDIR /Q/S "C:\\Bin\\GetTask3"')
                }

            }
        }
        stage('copyIisFiles') {
            steps {
                bat('xcopy "C:\\Program Files (x86)\\Jenkins\\workspace\\GetTask3\\ProductivityTools.GetTask3.Server\\bin\\Release\\netcoreapp3.1\\publish" "C:\\Bin\\GetTask3\\" /O /X /E /H /K')
            }
        }

        stage('startMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:GetTask3')
            }
        }
        stage('byebye') {
            steps {
                // Get some code from a GitHub repository
                echo 'byebye'
            }
        }
    }
}
