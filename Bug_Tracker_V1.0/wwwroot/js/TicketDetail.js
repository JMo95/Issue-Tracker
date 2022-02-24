var isChanged = false;

function setIsChanged() {
    isChanged = true;
    console.log(isChanged);
    console.log("is changed function called");
}

function displayUpdateBtn() {
    var updateBtn = document.getElementById('updateBtn');
    updateBtn.style.display = "inline";
    console.log("is changed function called");
    
         
}
