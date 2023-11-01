properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('hello') {
            steps {
                // Get some code from a GitHub repository
                echo 'hello!'
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
                git branch: 'main',
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
                bat('xcopy "ProductivityTools.GetTask3.Server.DbUp\\bin\\Release\\net7.0\\publish\\" "C:\\Bin\\GetTask3DdbMigration\\" /O /X /E /H /K')
            }
        }

        stage('runDbMigratorFiles') {
            steps {
                bat('C:\\Bin\\GetTask3DdbMigration\\ProductivityTools.GetTask3.Server.DbUp.exe')
            }
        }

        stage('stopPTTask3OnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:PTTasks3')
            }
        }
		
		stage('StopAppPool') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop apppool /apppool.name:"PTTasks3"')
            }
        }
		
		stage('Sleep') {
			steps {
				script {
					print('I am sleeping for a while!')
					sleep(30)    
				}
			}
		}
		stage('deleteIisDirFiles') {
            steps {
                retry(5) {
                    bat('if exist "C:\\Bin\\IIS\\PTTasks3" del /q "C:\\Bin\\IIS\\PTTasks3\\*"')
                }

            }
        }
        stage('deleteIisDir') {
            steps {
                retry(5) {
                    bat('if exist "C:\\Bin\\IIS\\PTTasks3" RMDIR /Q/S "C:\\Bin\\IIS\\PTTasks3"')
                }

            }
        }
        stage('copyIisFiles') {
            steps {
                bat('xcopy "Src\\Server\\ProductivityTools.GetTask3.API\\bin\\Release\\net7.0\\publish\\" "C:\\Bin\\IIS\\PTTasks3\\" /O /X /E /H /K')				              
            }
        }
		
		stage('Start AppPool') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start apppool /apppool.name:"PTTasks3"')
            }
        }
		

        stage('startMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:PTTasks3')
            }
        }
        stage('byebye') {
            steps {
                // Get some code from a GitHub repository
                echo 'byebye'
            }
        }
    }
	post {
		always {
            emailext body: "${currentBuild.currentResult}: Job ${env.JOB_NAME} build ${env.BUILD_NUMBER}\n More info at: ${env.BUILD_URL}",
                recipientProviders: [[$class: 'DevelopersRecipientProvider'], [$class: 'RequesterRecipientProvider']],
                subject: "Jenkins Build ${currentBuild.currentResult}: Job ${env.JOB_NAME}"
		}
	}
}
