pipeline {
    agent {
        label 'agent'
    }
    stages {
        stage('restore') {
            steps {
                bat 'dotnet restore'
            }
        }
        stage('build') {
            steps {
                bat 'dotnet build'
            }
        }
        stage('test') {
            steps {
                bat 'dotnet test'
            }
        }
    }
    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: 'OrangeHRM/bin/Debug/net7.0/allure-results']]
        }
    }
}
