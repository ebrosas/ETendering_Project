function ShowSupplierContactLookup(ajaxID, mode, contactID) {
	var win = window.radopen("SupplierContact.aspx?mode=" + mode + "&contactID=" + contactID +
		"&ajaxID=" + ajaxID, "winSupplierContactLookup");
	win.moveTo(0, 0);
	win.maximize();

	return false;
}

function OnCloseSupplierContactLookup(ajaxID, contactID, contactPrimary, newContact) {
	var objWin = GetRadWindow();
	objWin.BrowserWindow.UpdateSupplierContactLookup(ajaxID, contactID, contactPrimary, newContact);
	objWin.Close();

	return false;
}

function UpdateSupplierContactLookup(ajaxID, contactID, contactPrimary, newContact) {
	$find(ajaxID).ajaxRequest("UpdateSupplierContactLookUp;" + contactID + ";" + contactPrimary + ";" + newContact);

	return false;
}
