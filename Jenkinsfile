pipeline {
    agent {
        label 'agent'
    }
    stages {
        stage('restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('build') {
            steps {
                sh 'dotnet build'
            }
        }
        stage('test') {
            steps {
                sh 'dotnet test'
            }
        }
    }
    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: 'OrangeHRM/bin/Debug/net7.0/allure-results']]
        }
    }
}
