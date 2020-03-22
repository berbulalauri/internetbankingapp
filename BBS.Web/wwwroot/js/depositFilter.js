window.onload = function() {
    var depositContainer = document.getElementById("deposit-container"); //get deposite conatiner
    var myCurrency = depositContainer.querySelectorAll('[data-currency]'); //get all deposites
    var submitFilter = document.getElementById("submit-filter"); //get Find button
    var select = document.getElementById("LoanItem");//get drop down select for currency
    var currentSelect = select.options[select.selectedIndex].text; // get currency drop down selected value
    var selectLoanTerm = document.getElementById("LoanTerm"); //get drop down select for term
    var currentLoanTerm = selectLoanTerm.options[selectLoanTerm.selectedIndex].text; //get term drop down selected value
    var filterReset = document.getElementById("filter-reset"); //get Reset button
    var loanAmount = document.getElementById("MaxLoanSum"); // loan amount input
    var graphicalModules = document.getElementsByClassName("c100"); //get graphical presentation of loan percents
    
    submitFilter.onclick = filterLoans; //execute filter function after clicking FIND button

    myCurrency.forEach((element,index) => {//add clasnames for graphical percentage
        var graphicalValue = parseInt(element.getElementsByClassName("graph-circle")[0].innerHTML);
        graphicalModules[index].classList.add(`p${graphicalValue}`);
    });

    //filter function
    function filterLoans() {
        if(loanAmount.value == "" && currentLoanTerm == "Any Term") { // filter by currency
            
            myCurrency.forEach(element => {
                if(currentSelect.toUpperCase().indexOf(element.getAttribute('data-currency')) > -1) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }


                if(currentSelect == "Any Currency") { //if chosen value = all , show all desposites
                    element.style.display = " block";
                }
            });
            
        } 

        if (loanAmount.value != "" && currentSelect == "Any Currency" && currentLoanTerm == "Any Term") { //filter by amount
            myCurrency.forEach(element => {
                if(element.getElementsByClassName("loan-amount")[0].innerHTML  >= parseInt(loanAmount.value)) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }
            });
            
        } 

        if (loanAmount.value != "" && currentSelect != "Any Currency" && currentLoanTerm == "Any Term") { //filter by amount and currency
            myCurrency.forEach(element => {
                if(currentSelect.toUpperCase().indexOf(element.getAttribute('data-currency')) > -1 && element.getElementsByClassName("loan-amount")[0].innerHTML  >= parseInt(loanAmount.value)) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }
            });
        }

        if (loanAmount.value == "" && currentSelect == "Any Currency") { // filter by term
            myCurrency.forEach((element) => {
                var termValue = parseInt(element.getElementsByClassName("max-term")[0].innerHTML);
                
                if(parseInt(currentLoanTerm) == termValue) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }

                if(currentLoanTerm == "Any Term") { //if chosen value = all , show all desposites
                    element.style.display = " block";
                }
            });
        }

        if (loanAmount.value != "" && currentSelect == "Any Currency" && currentLoanTerm != "Any Term") { //filter by term and value
            myCurrency.forEach((element) => {
                var termValue = parseInt(element.getElementsByClassName("max-term")[0].innerHTML);
                
                if(parseInt(currentLoanTerm) == termValue && element.getElementsByClassName("loan-amount")[0].innerHTML  >= parseInt(loanAmount.value)) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }
            });
        }


        if (loanAmount.value == "" && currentSelect != "Any Currency" && currentLoanTerm != "Any Term") {//filter by term and currency
            myCurrency.forEach((element) => {
                var termValue = parseInt(element.getElementsByClassName("max-term")[0].innerHTML);
                
                if(parseInt(currentLoanTerm) == termValue
                && currentSelect.toUpperCase().indexOf(element.getAttribute('data-currency')) > -1
                ) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }
            });
        }

        if (loanAmount.value != "" && currentSelect != "Any Currency" && currentLoanTerm != "Any Term") { //filter by term, value and currency
            myCurrency.forEach((element) => {
                var termValue = parseInt(element.getElementsByClassName("max-term")[0].innerHTML);
                
                if(parseInt(currentLoanTerm) == termValue
                && element.getElementsByClassName("loan-amount")[0].innerHTML  >= parseInt(loanAmount.value) 
                && currentSelect.toUpperCase().indexOf(element.getAttribute('data-currency')) > -1
                ) { //chech if chosen value and deposite currency is the same
                    element.style.display = " block"; // show element if value and currency are the same
                } else {
                    element.style.display = "none"; // hide if are not the same
                }
            });
        }

    };
    
    //reset filter settings
     filterReset.onclick = function () {
      currentSelect = "Any Currency"; //set drop-down currency to any currency
      currentLoanTerm = "Any Term"; //set drop-down terms to any term
      loanAmount.value = ""; //set amount value to 0
      filterLoans(); //execute filter function
    };

    //get drop-down selected value after changing option
    select.onchange = function() { 
       currentSelect = select.options[select.selectedIndex].text;
    }
    selectLoanTerm.onchange = function() { 
        currentLoanTerm = selectLoanTerm.options[selectLoanTerm.selectedIndex].text;
     }
};



