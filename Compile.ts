
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

// compile each project
import * as child_process from 'child_process'

Projects.forEach(project => {
    const projectPath = project.projectFolder + "/" + project.projectName + ".csproj"
    const dllPath = project.projectFolder + "/bin/Debug/net8.0/" + project.dllName
    const outputPath = outputFolder + "/" + project.dllName

    console.log(`Compiling ${project.projectName}`)

    child_process.execSync(`dotnet build ${projectPath}`)
    fs.copyFileSync(dllPath, outputPath)

    console.log(`Compiled ${project.projectName}`)
})

console.log("Compilation complete")