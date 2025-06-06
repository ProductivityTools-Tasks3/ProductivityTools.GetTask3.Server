properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('Hello!') {
            steps {
                echo 'Hello Jenkins!'
            }
        }
        stage('Print workpalce Path'){
			steps{
				echo "${env.WORKSPACE}"
			}
		}
        stage('Print workpalce directory'){
            steps {
                deleteDir()
            }
        }

        stage('Git clone') {
            steps {
                // Get some code from a GitHub repository
                git branch: 'main',
                url: 'https://github.com/ProductivityTools-Tasks3/ProductivityTools.GetTask3.Server'
            }
        }
        stage('Build solution') {
            steps {
				echo 'starting build'
                bat('dotnet publish ProductivityTools.GetTask3.Server.sln -c Release')
            }
        }
        stage('Delete databse migration directory') {
            steps {
                bat('if exist "C:\\Bin\\DbMigration\\GetTask3DdbMigration" RMDIR /Q/S "C:\\Bin\\DbMigration\\GetTask3DdbMigration"')
            }
        }
        stage('Copy database migration files') {
            steps {
                bat('xcopy "ProductivityTools.GetTask3.Server.DbUp\\bin\\Release\\net9.0\\publish\\" "C:\\Bin\\DbMigration\\GetTask3DdbMigration\\" /O /X /E /H /K')
            }
        }

       stage('Run databse migration files') {
            steps {
                bat('C:\\Bin\\DbMigration\\GetTask3DdbMigration\\ProductivityTools.GetTask3.Server.DbUp.exe')
            }
        }

       stage('Create page on the IIS') {
            steps {
                powershell('''
                function CheckIfExist($Name){
                    cd $env:SystemRoot\\system32\\inetsrv
                    $exists = (.\\appcmd.exe list sites /name:$Name) -ne $null
                    Write-Host $exists
                    return  $exists
                }
                
                 function Create($Name,$HttpbBnding,$PhysicalPath){
                    $exists=CheckIfExist $Name
                    if ($exists){
                        write-host "Web page already existing"
                    }
                    else
                    {
                        write-host "Creating app pool"
                        .\\appcmd.exe add apppool /name:$Name /managedRuntimeVersion:"v4.0" /managedPipelineMode:"Integrated"
                        write-host "Creating webage"
                        .\\appcmd.exe add site /name:$Name /bindings:http://$HttpbBnding /physicalpath:$PhysicalPath
                        write-host "assign app pool to the website"
                        .\\appcmd.exe set app "$Name/" /applicationPool:"$Name"


                    }
                }
                Create "PTTasks" "*:8009"  "C:\\Bin\\IIS\\PTTask"                
                ''')
            }
        }
        stage('Stop PTJournal on IIS') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:PTTasks')
            }
        }

        stage('Delete PTTask IIS directory') {
            steps {
              powershell('''
                if ( Test-Path "C:\\Bin\\IIS\\PTTask")
                {
                    while($true) {
                        if ( (Remove-Item "C:\\Bin\\IIS\\PTTask" -Recurse *>&1) -ne $null)
                        {  
                            write-output "Removing failed we should wait"
                        }
                        else 
                        {
                            break 
                        } 
                    }
                  }
              ''')

            }
        }	
        stage('Copy web page to the IIS Bin directory') {
            steps {
                bat('xcopy "Src\\Server\\ProductivityTools.GetTask3.API\\bin\\Release\\net9.0\\publish\\" "C:\\Bin\\IIS\\PTTasks\\" /O /X /E /H /K')				              
            }
        }
	

        stage('Start website on IIS') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:PTTasks')
            }
        }
        stage('Create Login PTTask on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q "CREATE LOGIN [IIS APPPOOL\\PTTask] FROM WINDOWS WITH DEFAULT_DATABASE=[PTTask];"')
             }
        }

        stage('Create User PTTask on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q "USE PTTask;  CREATE USER [IIS APPPOOL\\PTTask]  FOR LOGIN [IIS APPPOOL\\PTTask];"')
             }
        }

        stage('Give DBOwner permissions on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q "USE PTTask;  ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\\PTTask];"')
             }
        }
        stage('Bye bye') {
            steps {
                // Get some code from a GitHub repository
                echo 'Bye bye'
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
