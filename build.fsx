#I "tools/FAKE"
#r "FakeLib.dll"

open Fake
open Fake.Git
open System.Linq
open System.Text.RegularExpressions
open System.IO

(* properties *)
let authors = ["Steffen Forkmann"; "Katarina Forkmann"]
let projectName = "AskMeItems"
let copyright = "Copyright - AskMeItems 2011"

(* Directories *)
let buildDir = @".\Build\"
let packagesDir = @".\packages\"
let docsDir = buildDir + @"Documentation\"
let testOutputDir = buildDir + @"Specs\"
let testDir = buildDir
let deployDir = @".\Release\"

(* files *)
let appReferences = !! @".\src\**\*.csproj"

(* flavours *)
let MSpecVersion = GetPackageVersion packagesDir "Machine.Specifications"
    
(* Targets *)
Target "Clean" (fun _ -> CleanDirs [buildDir; testDir; deployDir; docsDir; testOutputDir] )


Target "BuildApp" (fun _ -> 
    if not isLocalBuild then
      AssemblyInfo
        (fun p -> 
          {p with
              CodeLanguage = CSharp;
              AssemblyVersion = buildVersion;
              AssemblyTitle = "AskMeItems.Model";
              AssemblyDescription = "AskMeItems is a little questionnaire presenter and can be used for scientific studies.";
              AssemblyCopyright = copyright;
              Guid = "8cb588ea-29dd-4f12-9c1c-eb30ad1f7293";
              OutputFileName = @".\src\app\AskMeItems.Model\Properties\AssemblyInfo.cs"})
              
      AssemblyInfo
        (fun p -> 
          {p with
              CodeLanguage = CSharp;
              AssemblyVersion = buildVersion;
              AssemblyTitle = "AskMeItems.TextParser";
              AssemblyDescription = "AskMeItems is a little questionnaire presenter and can be used for scientific studies.";
              AssemblyCopyright = copyright;
              Guid = "564da14e-b022-40ae-9d47-6fd6793cd65a";
              OutputFileName = @".\src\app\AskMeItems.TextParser\Properties\AssemblyInfo.cs"})
              
      AssemblyInfo
        (fun p -> 
          {p with
              CodeLanguage = CSharp;
              AssemblyVersion = buildVersion;
              AssemblyTitle = "AskMeItems.WPF";
              AssemblyDescription = "AskMeItems is a little questionnaire presenter and can be used for scientific studies.";
              AssemblyCopyright = copyright;
              Guid = "03d98032-b4d0-463e-b87d-c9b82997c62b";
              OutputFileName = @".\src\app\AskMeItems.WPF\Properties\AssemblyInfo.cs"})                               

    appReferences
        |> MSBuildRelease buildDir "Build"
        |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    ActivateFinalTarget "DeployTestResults"
    !+ (testDir + "/*.Specs.dll")
      ++ (testDir + "/*.Examples.dll")
        |> Scan
        |> MSpec (fun p -> 
                    {p with 
                        ToolPath = sprintf @".\packages\Machine.Specifications.%s\tools\mspec-clr4.exe" MSpecVersion
                        HtmlOutputDir = testOutputDir})
)

FinalTarget "DeployTestResults" (fun () ->
    !+ (testOutputDir + "\**\*.*") 
      |> Scan
      |> Zip testOutputDir (sprintf "%sMSpecResults.zip" deployDir)
)

Target "BuildZip" (fun _ -> 
    !+ (buildDir + "/**/*.*")     
      -- "**/*.zip"
      -- "**/*.pdb"
      -- "**/*Spec*"
      -- "**/Specs/**"
        |> Scan
        |> Zip buildDir (deployDir + sprintf "%s-%s.zip" projectName buildVersion)
)

Target "Default" DoNothing
Target "Deploy" DoNothing

// Build order
"Clean"
  ==> "BuildApp"
  ==> "Test"
  ==> "BuildZip"
  ==> "Deploy"
  ==> "Default"

// start build
RunParameterTargetOrDefault  "target" "Default"
