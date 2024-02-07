function ShowSupplierOrderAlternativeLookup(ajaxID, forViewing, sodAltCurrencyCode, sodAltDelTerm, sodAltValidityPeriod,
		sodAltDeliveryTime, sodAltDeliveryPt) {
	var win = window.radopen("SupplierOrderAlternative.aspx?ajaxID=" + ajaxID + "&sodAltCurrencyCode=" + sodAltCurrencyCode +
		"&sodAltDelTerm=" + sodAltDelTerm + "&sodAltValidityPeriod=" + sodAltValidityPeriod + "&sodAltDeliveryTime=" + sodAltDeliveryTime + "&sodAltDeliveryPt=" + sodAltDeliveryPt +
		"&forViewing=" + forViewing, "winSupplierOrderAlternativeLookUp");
	win.moveTo(0, 0);
	win.maximize();

	return false;
}

function OnCloseSupplierOrderAlternativeLookup(ajaxID) {
	var objWin = GetRadWindow();
	objWin.BrowserWindow.UpdateSupplierOrderAlternativeLookup(ajaxID);
	objWin.Close();

	return false;
}

function UpdateSupplierOrderAlternativeLookup(ajaxID) {
	$find(ajaxID).ajaxRequest("UpdateSupplierOrderAlternativeLookup;");

	return false;
}
