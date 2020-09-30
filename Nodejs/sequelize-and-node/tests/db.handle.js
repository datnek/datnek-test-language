const util = require('util');
const exec = util.promisify(require('child_process').exec);
const dotenv = require("dotenv").config();

module.exports = () => {

    class DbHandle{

        constructor(){
            this.header = process.env.ACCESS_TOKEN;
        }
        async create(){
            await this.run("sequelize db:create");
        }

        async drop(){
            await this.run("sequelize db:drop");
        }

        async migrate(){
            await this.run("sequelize db:migrate");
        }

        async run(command) {
           try {
               const { stdout, stderr } = await exec(command);

               if (stderr) {
                   console.error(`error: ${stderr}`);
               }
               console.log(`stdout: ${stdout}`);
           } catch (e) {
               
           }
        }
    }
    return new DbHandle();
};
