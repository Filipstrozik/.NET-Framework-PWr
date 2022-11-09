var table = document.getElementById("multiplication");
var min = 1;
var max = 99;
var multiplicationRow = Array();
var multiplicationColumn = Array();


var n = window.prompt("Insert N number: ", "10");

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
    table.appendChild(document.createElement('tr'));
    for (let j = 0; j <= n; j++) {
        if (i == 0) {
            if (j > 0) {
                var td = document.createElement('th');
                td.innerHTML = multiplicationRow[j - 1];
                table.appendChild(td);
            } else {
                var td = document.createElement('th');
                td.innerHTML = 'n = ' + n;
                table.appendChild(td);
            }
        }
        else if (j == 0) {
            if (i > 0) {
                var td = document.createElement('th');
                td.innerHTML = multiplicationRow[i - 1];
                table.appendChild(td);
            }
        }
        else {
            var td = document.createElement('td');
            td.innerHTML = multiplicationRow[j - 1] * multiplicationColumn[i - 1];
            td.className = isEven(multiplicationRow[j - 1] * multiplicationColumn[i - 1]);
            table.appendChild(td);
        }
    }
}



function isEven(number) {
    if (number % 3 == 0) {
        return "even";
    } else if (number % 3 == 1) {
        return "odd";
    } else if (number % 3 == 2){
        return "sec";
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