properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('hello') {
            steps {
                // Get some code from a GitHub repository
                echo 'hello'
            }
        }
		stage('workplacePath'){
			steps{
				echo "${env.WORKSPACE}"
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
                url: 'https://github.com/ProductivityTools-Tasks3/ProductivityTools.GetTask3.Server'
            }
        }
        stage('build') {
            steps {
				echo 'starting bddduild'
                bat('dotnet publish ProductivityTools.GetTask3.Server.sln -c Release')
            }
        }
        stage('deleteDbMigratorDir') {
            steps {
                bat('if exist "C:\\Bin\\GetTask3DdbMigration" RMDIR /Q/S "C:\\Bin\\GetTask3DdbMigration"')
            }
        }
        stage('copyDbMigratorFiles') {
            steps {
                bat('xcopy "ProductivityTools.GetTask3.Server.DbUp\\bin\\Release\\netcoreapp3.1\\publish\\" "C:\\Bin\\GetTask3DdbMigration\\" /O /X /E /H /K')
            }
        }

        stage('runDbMigratorFiles') {
            steps {
                bat('C:\\Bin\\GetTask3DdbMigration\\ProductivityTools.GetTask3.Server.DbUp.exe')
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
                bat('xcopy "ProductivityTools.GetTask3.API\\bin\\Release\\netcoreapp3.0\\publish\\" "C:\\Bin\\GetTask3\\" /O /X /E /H /K')				              
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
