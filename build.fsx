// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open Fake.MSBuildHelper
open Fake.Testing.XUnit2

let buildDir  = @".\bin\"
let packagesDir = @".\packages"

let solutionFile  = "Formo.sln"

Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)

Target "Compile" (fun _ ->
    !! @"src\**\*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "XUnitTest" (fun _ ->
    !! (buildDir + @"\Formo.Tests.dll")
      |> xUnit2 (fun p ->
                 {p with ShadowCopy = true; HtmlOutputPath = Some (buildDir @@ "xunit.html") })
)

Target "All" DoNothing

"All"
  ==> "XUnitTest"

"Clean"
  ==> "Compile"
  =?> ("xUnitTest",hasBuildParam "xUnitTest") 

// start build
RunTargetOrDefault "XUnitTest"