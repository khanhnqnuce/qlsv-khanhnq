pf204652bottomLayer = document.getElementById('fl813691');
var pf204652IntervalId = 0;
var pf204652maxHeight = 342;
var pf204652minHeight = 20;
var pf204652curHeight = 0;
function pf204652show( ){
  pf204652curHeight += 2;
  if (pf204652curHeight > pf204652maxHeight){
	clearInterval ( pf204652IntervalId );
  }
  pf204652bottomLayer.style.height = pf204652curHeight+'px';
}
function pf204652hide( ){
  pf204652curHeight -= 3;
  if (pf204652curHeight < pf204652minHeight){
	clearInterval ( pf204652IntervalId );
  }
  pf204652bottomLayer.style.height = pf204652curHeight+'px';
}
pf204652IntervalId = setInterval ( 'pf204652show()', 5 );
function pf204652clickhide(){
	document.getElementById('pf204652hide').style.display='none';
	document.getElementById('pf204652show').style.display='inline';
	pf204652IntervalId = setInterval ( 'pf204652hide()', 5 );
}
function pf204652clickshow(){
	document.getElementById('pf204652hide').style.display='inline';
	document.getElementById('pf204652show').style.display='none';
	pf204652IntervalId = setInterval ( 'pf204652show()', 5 );
}
function pf204652clickclose(){
	document.body.style.marginBottom = '0px';
	pf204652bottomLayer.style.display = 'none';
}
