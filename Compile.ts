
type Project = {
    projectFolder: string,
    projectName: string,
    dllName: string,
}
// list of all .net projects that need to be compiled
const Projects: Array<Project> = [
    {
        "dllName": "PersistentChalk.dll",
        "projectFolder": "PersistentChalk",
        "projectName": "PersistentChalk"
    },
    {
        "dllName": "StaticRain.dll",
        "projectFolder": "StaticRain",
        "projectName": "StaticRain"
    }
]

console.log("Compiling projects...")

// for each project, compile the project and copy the dll to the output folder

// create output folder
import * as fs from 'fs'

const outputFolder = "plugins"
if (!fs.existsSync(outputFolder)) {
    fs.mkdirSync(outputFolder)
}

// fetch the cove git repository
// https://github.com/DrMeepso/WebFishingCove/tree/main

// clone the repository
import * as child_process from 'child_process'

console.log("Cloning WebFishingCove repository...")
child_process.spawnSync("git", ["clone", "https://github.com/DrMeepso/WebFishingCove.git"])
console.log("Cloned WebFishingCove repository")

// compile each project

Projects.forEach(project => {
    const projectPath = project.projectFolder + "/" + project.projectName + ".csproj"
    const dllPath = project.projectFolder + "/bin/Debug/net8.0/" + project.dllName
    const outputPath = outputFolder + "/" + project.dllName

    console.log(`Compiling ${project.projectName}`)

    // spawn a new process to compile the project
    child_process.spawnSync("dotnet", ["restore", projectPath])
    const compileProcess = child_process.spawnSync("dotnet", ["build", projectPath])
    // print the output of the compilation
    compileProcess.stdout && console.log(compileProcess.stdout.toString())
    fs.copyFileSync(dllPath, outputPath)

    console.log(`Compiled ${project.projectName}`)
})

console.log("Compilation complete")