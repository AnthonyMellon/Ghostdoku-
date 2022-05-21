fs = require('fs');

let board;
let finalBoard;
let numDigits = 9;
let minEmptyCells = 20;
let maxEmptyCells = 30;
let sqrtNumDigits = Math.sqrt(9);
let clearAttempts = 0;
const maxClearAttemps = 1000;

const getSudoku = () => {

    board = new Array();
    for(let i = 0; i < numDigits*numDigits; i++)
    {
        board.push(newNumSet());
    }
    
    finalBoard = new Array(numDigits*numDigits).fill(0);
    fillBoard();    
    clearAttempts = 0;

     if(!boardIsValid()) getSudoku();
     else clearCells();
}

const clearCells = () => {
    let clearedBoard = new Array(numDigits*numDigits).fill(0);
    copyArray(finalBoard, clearedBoard);
    let numRemoved = 0;
    let numToRemove = Math.random() * (maxEmptyCells - minEmptyCells) + minEmptyCells;

    while (numRemoved < numToRemove) //Clear n amount of cells
    {
        //Get a random index thats not already been cleared and set clear it
        let index = 0;
        do {
            index = Math.floor(Math.random()*clearedBoard.length);
        } while(clearedBoard[index] == 0);

        clearedBoard[index] = 0;
        numRemoved++;
    }

    if(isSolvable(clearedBoard)) {
        //console.log(`Made a solvable puzzle with ${numEmptyCells} empty spaces after ${clearAttempts} attempts`);
        copyArray(clearedBoard, finalBoard);
    } else {
        clearAttempts++;

        if(clearAttempts >= maxClearAttemps) {
            //console.log(`Could not make a solvable puzzle with ${numEmptyCells} empty spaces after ${clearAttempts} attempts`);
        } else {
            clearCells();
        }
    }
}

const isSolvable = (board) => {
    let testboard = new Array(numDigits*numDigits).fill(0);
    copyArray(board, testboard);
    let emptyCells = [];

    //Find all the empty cells indexes
    for(let i = 0; i < testboard.length; i++) {
        if(testboard[i] === 0) emptyCells.push(i);
    }

    let index = 0; //The current cell being looked at
    let solvable = true;
    do {
        //Find the possible numbers to fill the cell
        let possibleNums = [];
        for(let i = 1; i <= numDigits; i++) {
            if(!numConflicts(emptyCells[index], i, testboard)) possibleNums.push(i);
        }
        //If theres only 1 possible number to fill this cell
        if(possibleNums.length == 1) {
            testboard[emptyCells[index]] = possibleNums[0]; //Insert number into cell
            emptyCells.splice(index, 1); //Remove the index from empty cells list
            index = 0; //Start the loop over
        }

        //If theres more than 1 possible number to fill this cell
        else {
            //If this is the last empty cell
            if(index === emptyCells.length - 1) solvable = false; //Puzzle is not solvable   

            //If this is not the last empty cell
            else index++; //Move to next cell
        }

    } while(solvable && emptyCells.length > 0)

    return solvable;
}

const fillBoard = () => {
    let index = 0;
    while(index < board.length)
    {
        //Start
        if(index < 0) return;

        //Are there available numbers for this cell?
        if(board[index].length > 0) { //Yes there are available numbers
            if(!numConflicts(index, board[index][0], finalBoard)) { //If the first available number does not conflict
                finalBoard[index] = board[index][0]; //Use the first available number  
                board[index].splice(0, 1);              
                index++; //Move to the next cell
            }
            else { //If the first available number does conflict
                board[index].splice(0, 1); //Remove first available number from list of available numbers
            }
        }
        else { //No there are no available numbers
            board[index] = newNumSet(); //Refresh this cells available numbers
            index--; //Go back 1 cell and try a new number
        }
    }
}

const numConflicts = (index, num, checkboard) => {
    if(isInRow(num, indexToRow(index), checkboard) || isInCol(num, indexToCol(index), checkboard) || isInBox(num, indexToBox(index), checkboard)) return true;
    return false;
}

const isInRow = (num, row, checkboard) => {
    for(let i = 0; i < board.length; i++) { //Loop through the whole board
        if(Math.floor(i / numDigits) == row && num == checkboard[i]) return true;
    }
    return false;
}

const isInCol = (num, col, checkboard) => {
    for(let i = 0; i < board.length; i++) { //Loop through the whole board
        if(i % numDigits == col && num == checkboard[i]) return true;
    }
    return false;
}

const isInBox = (num, box, checkboard) => {
    for(let i = 0; i < numDigits; i++) {
        let index = Math.floor(i / sqrtNumDigits);
        index *= numDigits;
        index += i %sqrtNumDigits;
        index += (box % sqrtNumDigits) * sqrtNumDigits; //Horizontal offset
        index += (Math.floor(box / sqrtNumDigits)) * (numDigits * sqrtNumDigits);

        if(checkboard[index] == num) return true;
    }
    return false;
}

const indexToRow = (index) => {
    return Math.floor(index / numDigits);
}

const indexToCol = (index) => {
    return index % numDigits;
}

const indexToBox = (index) => {
    let boxCol = Math.floor(indexToCol(index) / sqrtNumDigits);
    let boxRow = Math.floor(indexToRow(index) / sqrtNumDigits) * sqrtNumDigits;
    return boxCol + boxRow;
}

const newNumSet = () => {
    let numSet = [];
    for(let i = 0; i < numDigits; i++) {
        numSet.push(i + 1);
    }
    numSet = shuffleArray(numSet);
    return numSet;
}

const shuffleArray = (myArray) => {
    //Fisher-Yates shuffle
    for(let i = 0; i < myArray.length; i++) {
        let j = Math.floor(Math.random()*i);
        if(j != i) {
            let temp = myArray[j];
            myArray[j] = myArray[i];
            myArray[i] = temp;
        }
    }
    return myArray;
}

const copyArray = (from, to) => {
    for(let i = 0; i < from.length; i++) {
        to[i] = from[i];
    }
}

const boardIsValid = () => {

    let valid = true;

    (finalBoard).forEach(num => {
        if(num === 0) valid = false;
    });
    return valid;
}

const testPassRate = (sampleSize) => {
    let passes = 0;
    for(let i = 0; i < sampleSize; i++) {
        getSudoku(numDigits, minEmptyCells);
        if(boardIsValid()) {            
            passes++;                
        }     
        console.log(`${i} of ${sampleSize}`)    
    }

    let passPercent = (passes / sampleSize) * 100;
    console.log(`Generated ${sampleSize} Sudokus\n${passes} of ${sampleSize} (${passPercent}%) generated successfuly`);
}

const massSudoku = (nSudokus, tminEmptyCells, tmaxEmptyCells, puzzleFile, replace) => {

    minEmptyCells = tminEmptyCells;
    maxEmptyCells = tmaxEmptyCells;
    let fileName = puzzleFile;
    if(replace) {
        fs.writeFile(fileName, '', (e) => {
            if(e) console.log(e);
        });
    }


    let totalTime = 0;
    for(let i = 0; i < nSudokus; i++) {
        let startTime = new Date();
        getSudoku(numDigits, tminEmptyCells);
        fs.appendFileSync(fileName, `${finalBoard.toString()}\n`, (e) => {
            if(e) console.log(e);
        });
        
        let endTime = new Date();
        let deltaTime = endTime - startTime;
        totalTime += deltaTime;
        console.log(`Successfuly made sudoku #${i+1} in ${deltaTime}ms`);        
    }
    console.log(`Average time to make sodoku: ${totalTime / nSudokus}ms`);
    console.log(`Total time spent: ${totalTime / 1000}secs`)
}

massSudoku(1, 3, 3, 'sudoku-veryEasy.txt', true);
//testPassRate(1000);