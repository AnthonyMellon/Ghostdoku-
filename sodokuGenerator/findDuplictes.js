fs = require('fs');
puzzleList = 'sudoku-easy.txt';

const readFile = () => {
    fs.readFile(puzzleList, 'utf8', (err, data) => {
        if(err) return err;

        const cleanData = removeDuplicates(parseData(data)); 

        fs.writeFile(puzzleList, '', (e) => { //blank the file
            if(e) console.log(e);
        }); 

        cleanData.forEach(element => { //Insert the clean data into the file
            fs.appendFile(puzzleList, `${element.toString()}\n`, (e) => {
                if(e) console.log(e);
            });  
        });   
  
    });
}

const parseData = (data) => {
    return (data.split('\n'));
}

const removeDuplicates = (data) => {
    let cleanData = [];
    for(let i = 0; i < data.length - 1; i++) //Loop through each sudoku puzzle
    {
        let start = new Date();
        let duplicate = false;
        cleanData.forEach(element => {
            if(data[i].toString() == element.toString()) {                
                duplicate = true;
            }
        });

        if(!duplicate) cleanData.push(data[i])

        let end = new Date();
        console.log(`Check sudoku ${i+1}/${data.length-1} in ${end - start}ms`);
    }
    console.log(`${cleanData.length} unique sudokus`);

    return cleanData;
}

readFile();

