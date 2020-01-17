#!groovy
properties([disableConcurrentBuilds()])

pipeline{
    agent any

    parameters{
        string(name: 'PARATERES', defaultValue: 'development', description: '' )
    }

    options{
        buildDiscarder(logRotator(numToKeepStr: '5'))
        timestamps()
    }

    stages{
        stage("Build"){
            steps{
                steps{
                    echo "Build should be emplemented...soon"
            }
            }
        }
        stage('Test'){
            steps{
                echo "Tests should be emplemented...soon"
            }
        }
        stage('Deploy'){
            when {
                expression {
                    return true;
                }
            }
            steps{
                script{
                    stdout = powershell(returnStdout: true, script: '''
                        exit 0
                    ''')
                }
            }
        }
        post{
            success{
                //sendSuccess("${params.SLACK_NOTIFY}")
            }
            failure{
                //sendFailing("${params.SLACK_NOTIFY}")
            }
            aborted{
                //slackSend(channel: "${params.SLACK_NOTIFY}", message: "CI skipped")
            }
        }
}