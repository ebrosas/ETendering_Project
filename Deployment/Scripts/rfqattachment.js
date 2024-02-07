function ShowRFQDetailAttachmentLookup(ajaxID, orderDetNo, orderDetLineNo, orderAttachSODID, orderAttachSODAltID, orderDetSupplier, orderDetAttachType, forViewing) {
	var win = window.radopen("../CommonObject/RFQDetailAttachment.aspx?orderDetNo=" + orderDetNo + "&orderDetLineNo=" + orderDetLineNo +
		"&orderAttachSODID=" + orderAttachSODID + "&orderAttachSODAltID=" + orderAttachSODAltID + "&orderDetSupplier=" + orderDetSupplier +
		"&orderDetAttachType=" + orderDetAttachType + "&forViewing=" + forViewing + "&ajaxID=" + ajaxID, "winRFQDetailAttachmentLookUp");
	win.moveTo(0, 0);
	win.maximize();

	return false;
}

function ShowSupplierOrderAttachmentLookup(ajaxID, orderDetAttachType, orderDetAlt, forViewing) {
	var win = window.radopen("SupplierOrderAttachment.aspx?orderDetAttachType=" + orderDetAttachType + "&orderDetAlt=" + orderDetAlt + "&forViewing=" + forViewing +
		"&ajaxID=" + ajaxID, "winRFQDetailAttachmentLookUp");
	win.moveTo(0, 0);
	win.maximize();

	return false;
}

function OnCloseSupplierOrderAttachmentLookup(ajaxID) {
	var objWin = GetRadWindow();
	objWin.BrowserWindow.UpdateSupplierOrderAttachmentLookup(ajaxID);
	objWin.Close();

	return false;
}

function UpdateSupplierOrderAttachmentLookup(ajaxID) {
	$find(ajaxID).ajaxRequest("UpdateSupplierOrderAttachmentLookup;");

	return false;
}
