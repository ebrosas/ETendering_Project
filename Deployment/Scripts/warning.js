function ShowWarningMsg(ajaxID, buttonID, warningMsg) {
	var win = window.radopen("../CommonObject/ConfirmDialog.aspx?buttonID=" + buttonID + "&warningMsg=" + warningMsg + "&ajaxID=" + ajaxID,
		"winWarningMsg");
	win.setSize(400, 200);
	win.center();

	return false;
}

function OnCloseWarningMsg(ajaxID, buttonID, reply) {
	var objWin = GetRadWindow();
	objWin.BrowserWindow.UpdatWarningMsg(ajaxID, buttonID, reply);
	objWin.Close();

	return false;
}

function UpdatWarningMsg(ajaxID, buttonID, reply) {
	$find(ajaxID).ajaxRequest("ConfirmedWindow;" + buttonID + ";" + reply);
}

function ShowSuccessMsg(ajaxID, buttonID, successMsg) {
	var win = window.radopen("../CommonObject/SuccessDialog.aspx?buttonID=" + buttonID + "&successMsg=" + successMsg + "&ajaxID=" + ajaxID,
		"winWarningMsg");
	win.setSize(400, 200);
	win.center();

	return false;
}

function OnCloseSuccessMsg(ajaxID, buttonID, reply) {
	var objWin = GetRadWindow();
	objWin.BrowserWindow.UpdateSuccessMsg(ajaxID, buttonID, reply);
	objWin.Close();

	return false;
}

function UpdateSuccessMsg(ajaxID, buttonID, reply) {
	$find(ajaxID).ajaxRequest("ConfirmedWindow;" + buttonID + ";" + reply);
}
