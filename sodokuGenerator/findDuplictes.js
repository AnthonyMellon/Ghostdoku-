fs = require('fs');
puzzleList = process.argv[2];

const readFile = () => {

    if(!process.argv[2]) return console.log('please provide a file');

    fs.readFile(puzzleList, 'utf8', (err, data) => {
        if(err) return console.log(err);

        let start = new Date();

        const cleanData = removeDuplicates(parseData(data)); 

        fs.writeFile(puzzleList, '', (e) => { //blank the file
            if(e) console.log(e);
        }); 

        cleanData.forEach(element => { //Insert the clean data into the file
            fs.appendFileSync(puzzleList, `${element.toString()}\n`, (e) => {
                if(e) console.log(e);
            });  
        }); 
        
        console.log(`Total time spent: ${(new Date() - start) / 1000}secs`);
  
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
        console.log(`Check sudoku ${i+1}/${data.length-1} in ${end - start}ms | duplicate: ${duplicate}`);
    }
    console.log('------------------');
    console.log(`${data.length-1} sudokus checked\n${data.length-1 - cleanData.length} duplicates found and removed\n${cleanData.length} sudokus remain`);
    console.log('------------------');

    return cleanData;
}

readFile();

