﻿var table = document.getElementById("multiplication");
var output = "";
var min = 1;
var max = 99;
var multiplicationRow = Array();
var multiplicationColumn = Array();


var n = window.prompt("Insert N number: ", "10");
// console.log(typeof n);

while (isNaN(n)) { // NaN means Not a Number check if it is
    alert('It is not a number, please type a number from 5 to 20.');
    n = window.prompt("Insert N number: ");
}
if (n < 5 || n > 20) {
    var result = confirm("Number is out of bound 5-20! N number is set to default value = 10.")
    n = 10;
}

randomFillArray(multiplicationRow, n, min, max);
randomFillArray(multiplicationColumn, n, min, max);
// console.log(multiplicationRow);
// console.log(multiplicationColumn);

for (let i = 0; i <= n; i++) {
    output += "<tr>";
    for (let j = 0; j <= n; j++) {
        if (i == 0) {
            if (j > 0) {
                output += "<th>" + multiplicationRow[j - 1] + "</th>";
            } else {
                output += "<th>n=" + n + "</th>"
            }
        }
        else if (j == 0) {
            if (i > 0) {
                output += "<th>" + multiplicationColumn[i - 1] + "</th>";
            }
        }
        else {
            // console.log("row: "+multiplicationRow[i-1]+
            //             "column: "+ multiplicationColumn[j-1]+
            //             "result: " +multiplicationRow[i-1]*multiplicationColumn[j-1])
            output += isEven(multiplicationRow[j - 1] * multiplicationColumn[i - 1]);
        }
    }
    output += "</tr>";
}
table.innerHTML = output;



function isEven(number) {
    if (number % 2 == 0) {
        return '<td class="even">' + number + "</td>";
    } else {
        return '<td class="odd">' + number + "</td>";
    }
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

function randomFillArray(arr, n, min, max) {
    for (let i = 0; i < n; i++) {
        arr.push(getRandomInt(min, max));
    }
}