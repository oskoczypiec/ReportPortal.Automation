package _Self.buildTypes

import jetbrains.buildServer.configs.kotlin.*
import jetbrains.buildServer.configs.kotlin.buildFeatures.notifications
import jetbrains.buildServer.configs.kotlin.buildFeatures.perfmon
import jetbrains.buildServer.configs.kotlin.buildSteps.dotnetBuild
import jetbrains.buildServer.configs.kotlin.buildSteps.dotnetTest
import jetbrains.buildServer.configs.kotlin.buildSteps.nuGetInstaller
import jetbrains.buildServer.configs.kotlin.buildSteps.script
import jetbrains.buildServer.configs.kotlin.triggers.schedule
import jetbrains.buildServer.configs.kotlin.triggers.vcs

object Build : BuildType({
    name = "Build"

    vcs {
        root(HttpsGithubComOskoczypiecReportPortalAutomationRefsHeadsMain)
    }

    steps {
        nuGetInstaller {
            id = "jb_nuget_installer"
            toolPath = "%teamcity.tool.NuGet.CommandLine.DEFAULT%"
            projects = "ReportPortal.Automation/ReportPortal.Automation.sln"
            updatePackages = updateParams {
            }
        }
        script {
            name = "SonarScanner run"
            id = "SonarScanner_run"
            scriptContent = """SonarScanner.MSBuild.exe begin /k:"ReportPortal.SonarQube" /d:sonar.login="""""
        }
        dotnetBuild {
            id = "dotnet"
            projects = "ReportPortal.Automation/ReportPortal.Automation.sln"
            sdk = "6"
        }
        dotnetTest {
            id = "dotnet_1"
            projects = "ReportPortal.Automation/ReportPortal.Tests/ReportPortal.Tests.csproj"
            sdk = "6"
        }
        dotnetTest {
            id = "dotnet_2"
            projects = "ReportPortal.Automation/ReportPortal.Tests.API/ReportPortal.Tests.API.csproj"
            sdk = "6"
        }
        dotnetTest {
            id = "dotnet_3"
            projects = "ReportPortal.Automation/ReportPortal.BDD.Tests/ReportPortal.BDD.Tests.csproj"
            sdk = "6"
        }
        script {
            name = "SonarScanner end"
            id = "SonarScanner_end"
            scriptContent = """SonarScanner.MSBuild.exe end /d:sonar.login="""""
        }
    }
    triggers {
        vcs {
            branchFilter = "+:<default>"
        }
        schedule {
            branchFilter = """
                +:*
                +:<default>
            """.trimIndent()
            triggerBuild = always()
        }
    }
    features {
        perfmon {
        }
        notifications {
            notifierSettings = emailNotifier {
                email = """
                    aleksandra_skoczypiec@epam.com
                    o.skoczypiec@gmail.com
                """.trimIndent()
            }
            buildStarted = true
            buildFailedToStart = true
            buildFailed = true
            buildFinishedSuccessfully = true
            firstBuildErrorOccurs = true
            buildProbablyHanging = true
            queuedBuildRequiresApproval = true
        }
    }
})
