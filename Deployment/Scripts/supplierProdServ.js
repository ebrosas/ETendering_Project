function ShowSupplierProdServLookUp(ajaxID, prodServCode, prodServCodeDesc) {
	var win = window.radopen("../CommonObject/SupplierProdServLookup.aspx?prodServCode=" + prodServCode + "&prodServCodeDesc=" + prodServCodeDesc +
		"&ajaxID=" + ajaxID, "winProdServ");
	win.moveTo(0, 0);
	win.maximize();

	return false;
}

function OnCloseSupplierProdServLookUp(ajaxID, prodServCode) {
	var objWin = GetRadWindow();
	objWin.BrowserWindow.UpdateSupplierProdServLookUp(ajaxID, prodServCode);
	objWin.Close();

	return false;
}

function UpdateSupplierProdServLookUp(ajaxID, prodServCode) {
	$find(ajaxID).ajaxRequest("UpdateSupplierProductServiceLookUp;" + prodServCode);

	return false;
}
