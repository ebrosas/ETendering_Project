// ------------------------------------------------------------------------------------------------ //
function showLoadingPage(ajaxID, loadID) {

	// Retrieve the hidden load flag
	var loading = document.getElementById(loadID);
	if (loading.value == 0)
		__doPostBack(ajaxID, "PageLoading");
}

// Application Window
function ShowApplication(url) {
	var width = 0;
	var height = 0;

	var garmcoB2B = window.open(url, "GRMB2B",
		"menubar=no, status=1, resizable=no, scrollbars=no, toolbar=no, top=0, left=0");

	// Close the main window opener
	if (garmcoB2B != null) {

		garmcoB2B.focus();

		window.open('', '_parent', '');
		window.close();

	}

	if (document.all) {

		garmcoB2B.resizeTo(screen.availWidth, screen.availHeight);
		//top.window.resizeTo(screen.availWidth,screen.availHeight);

	}

	else if (document.layers || document.getElementById) {
		if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {

			garmcoB2B.outerHeight = screen.availHeight;
			garmcoB2B.outerWidth = screen.availWidth;
			//		top.window.outerHeight = screen.availHeight;
			//		top.window.outerWidth = screen.availWidth;

		}
	}
}

function CloseApplication() {
	window.parent.close();
}
// ------------------------------------------------------------------------------------------------ //

var currentClientHeight = 0;
var currentClientWidth = 0;
function ConfigureClientHeight() {
	var mainBody;
	if (navigator.appName == "Microsoft Internet Explorer") {

		currentClientHeight = document.documentElement.clientHeight;
		currentClientWidth = document.documentElement.clientWidth;

		// Modify the height of the content placement
		mainBody = document.getElementById("mainBody");
		mainBody.style.height = String(currentClientHeight - 170) + "px";

	}

	else if (navigator.appName == "Netscape") {

		currentClientHeight = window.innerHeight;
		currentClientWidth = window.innerWidth;

		// Modify the height of the content placement
		document.getElementById("mainBody").style.height = String(currentClientHeight - 175) + "px";

	}
}

function SetWindowSize() {
	ConfigureClientHeight();

	// Set the date and time
	SetDateTime();
}

function SetDateTime() {
	var dayList = new Array("Sunday", "Monday	", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
	var monthList = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September",
								"October", "November", "December");

	var currentDateTime = new Date();

	var dayWeek = currentDateTime.getDay();
	var day = currentDateTime.getDate();
	var month = currentDateTime.getMonth();
	var year = currentDateTime.getFullYear();
	var hours = currentDateTime.getHours();
	var mins = currentDateTime.getMinutes();
	var seconds = currentDateTime.getSeconds();

	// Display the time
	var dateTime = new String(dayList[dayWeek] + ", " + FormatNumber(day) + " " + monthList[month] + " " + year + " " +
			FormatNumber(hours) + ":" + FormatNumber(mins) + ":" + FormatNumber(seconds));

	//	if (navigator.appName == "Microsoft Internet Explorer")
	document.getElementById("currentDateTime").innerHTML = dateTime;

	// Set the timeout
	setTimeout('SetDateTime()', 1000);
}

function FormatNumber(number) {
	var newNumber = new String(number);

	if (number < 10)
		newNumber = "0" + number;

	return newNumber;
}

// ------------------------------------------------------------------------------------------------ //
// Activating a window
var activeWin = null;
function OnClientShow(sender, args) {
	// Set the active window
	activeWin = sender;
}

function OnClientActivate(sender, args) {
	//	if (activeWin) {

	//		var formerZindex = activeWin.get_popupElement().style.zIndex;
	//		var currentZindex = sender.get_popupElement().style.zIndex;
	//		if (currentZindex <= formerZindex)
	//			sender.get_popupElement().style.zIndex = formerZindex + 1000;

	//	}

	activeWin = sender;
}

function setActiveWindow(win) {
	var rootDiv = win._popupElement;
	var oldZIndex = parseInt(rootDiv.style.zIndex);
	var newZIndex = (win.get_stylezindex()) ? win.get_stylezindex() : Telerik.Web.UI.RadWindowUtils.get_newZindex(oldZIndex);
	rootDiv.style.zIndex = "" + newZIndex;
}

function OnClientItemClicked(sender, args) {
	var itemVal = args.get_item().get_value();
	if (itemVal != null)
		eval(itemVal);
}
// ------------------------------------------------------------------------------------------------ //

function GetRadWindow() {
	var oWindow = null;
	if (window.radWindow)
		oWindow = window.radWindow;

	else if (window.frameElement.radWindow)
		oWindow = window.frameElement.radWindow;

	return oWindow;
}

function OnStatusDialogCallBack(radWindow, returnValue) {
	if (returnValue != "")
		window.location.href = returnValue;

	return false;
}

function OnCloseWindow() {
	GetRadWindow().Close();
	return false;
}

function OnCancelWindow() {
	GetRadWindow().Close();
	return false;
}

function SetWindowTitle(winTitle) {
	GetRadWindow().set_title(winTitle);
}

function ShowAlertMessage(msg) {
	radalert(msg, 0, 0, "Error Message");

	return false;
}

function ShowAlertMessageWithDelay(msg) {
	function f() {
		Sys.Application.remove_load(f);
		ShowAlertMessage(msg);
	}

	Sys.Application.add_load(f);
}

var ajaxMngrID = "";
var btnClicked = 0;
var returnConfirm = 0;
function ShowConfirmMessageWithButtonIndicator(msg, ajaxID, btnAction) {
	// Set which button that was pressed
	btnClicked = btnAction;

	// Show confirmation message
	ShowConfirmMessage(msg, ajaxID);
}

function ShowConfirmMessageWithuBttonIndicatorRetArg(msg, ajaxID, btnAction, retConfirm) {
	// Set which button that was pressed
	btnClicked = btnAction;

	ajaxMngrID = ajaxID;
	returnConfirm = retConfirm;
	radconfirm(msg, ConfirmCallBack, 0, 0, null, "Warning");

	return false;
}

function ShowConfirmMessage(msg, ajaxID) {

	ajaxMngrID = ajaxID;
	radconfirm(msg, ConfirmCallBack, 0, 0, null, "Warning");

	return false;
}

function ShowConfirmMessage(msg, ajaxID, retConfirm) {

	ajaxMngrID = ajaxID;
	returnConfirm = retConfirm;
	radconfirm(msg, ConfirmCallBack, 0, 0, null, "Warning");

	return false;
}

function ConfirmCallBack(arg) {
	// If returned value is true
	var callbackFunction = "ConfirmedWindow";
	// Check if it will return regardless of the action
	if (returnConfirm == 1) {

		// Checks if button was set
		if (btnClicked != 0)
			$find(ajaxMngrID).ajaxRequest(callbackFunction + ";" + btnClicked + ";" + arg, null);

		else
			$find(ajaxMngrID).ajaxRequest(callbackFunction + ";" + arg, null);

	}

	else if (arg == true) {

		// Checks if button was set
		if (btnClicked != 0)
			$find(ajaxMngrID).ajaxRequest(callbackFunction + ";" + btnClicked, null);

		else
			$find(ajaxMngrID).ajaxRequest(callbackFunction, null);

	}

	return false;
}

function InitiateAsyncRequest(ajaxMngrID) {
	var ajaxManager = $find(ajaxMngrID);
	if (ajaxManager != null)
		ajaxManager.ajaxRequest(null);

	return false;
}
