var graphicalModules = document.getElementsByClassName("c100")[0];
var graphCircles = document.getElementsByClassName("graph-circle")[0];
var graphicalValue = parseInt(graphCircles.innerHTML);
graphicalModules.classList.add(`p${graphicalValue}`);
