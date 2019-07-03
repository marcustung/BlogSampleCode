// LINQ
int[] myNumbers = { 1, 30, 26, 53, 97, };
int myRemoveNum = 53;
myNumbers = myNumbers.Where(val => val != myRemoveNum).ToArray();

// None LINQ
int[] myNumbers = { 1, 30, 26, 53, 97, };
int myRemoveNum = 53;
myNumbers = Array.FindAll(myNumbers, val => val != myRemoveNum).ToArray();