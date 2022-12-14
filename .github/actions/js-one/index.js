const core = require('@actions/core');
const github = require('@actions/github');
const fs = require('fs');


try {
    // assume the best
    core.setOutput("verified", true);
    // `root-lab-path` input defined in action metadata file
    const rootPath = core.getInput('root-lab-path');
    console.log(`root-lab-path ${rootPath}`);
    const dirContents = fs.readdirSync(rootPath);
    console.log(`folders and files ${dirContents}!`);

    //for each entry in folders
    for (var i = 0; i < dirContents.length; i++) {
        const path_string = rootPath + "/" + dirContents[i];
        const isDirectory = fs.lstatSync(path_string).isDirectory();
        console.log(`path_string ${path_string} | directory ${isDirectory}`);
        const readmePath = path_string + "/README.md"
        const readmeExists = fs.existsSync(readmePath);
        console.log(`readme ${readmePath} | exists ${readmeExists}`);
        if (!readmeExists) {
            core.setOutput("verified", false);
            core.setFailed(`${path_string} does not have a README.md`);
        }
    }

} catch (error) {
    core.setFailed(error.message);
}
