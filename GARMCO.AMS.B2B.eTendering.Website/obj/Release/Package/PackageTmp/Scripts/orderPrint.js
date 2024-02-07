function ShowPrintSupplierOrder(printType, sodOrderNo, sodSupplierName, sodOrderClosingDate, sodCurrencyCode, sodDeliveryTerm,
	sodValidityPeriod, sodDeliveryTime, sodDeliveryPt, sodPaymentTerm, sodBuyer, sodOrderStatus) {
	var win = window.radopen("../CommonObject/OrderPrint.aspx?printType=" + printType +
		"&sodOrderNo=" + sodOrderNo + "&sodSupplierName=" + sodSupplierName + "&sodOrderClosingDate=" + sodOrderClosingDate +
		"&sodCurrencyCode=" + sodCurrencyCode + "&sodDeliveryTerm=" + sodDeliveryTerm + "&sodValidityPeriod=" + sodValidityPeriod +
		"&sodDeliveryTime=" + sodDeliveryTime + "&sodDeliveryPt=" + sodDeliveryPt + "&sodPaymentTerm=" + sodPaymentTerm +
		"&sodBuyer=" + sodBuyer + "&sodOrderStatus=" + sodOrderStatus, "winPrintOrder");

	win.moveTo(0, 0);
	win.maximize();

	return false;
}

function ShowPrintPublishRFQ(printType, orderNo) {
	var win = window.radopen("../CommonObject/OrderPrint.aspx?printType=" + printType +
		"&orderNo=" + orderNo, "winRFQ");

	win.moveTo(0, 0);
	win.maximize();

	return false;
}

