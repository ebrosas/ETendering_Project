
	SELECT * FROM b2badminuser.SupplierOrder a WITH (NOLOCK)
	WHERE a.SupOrderOrderNo = 40085193
	
	SELECT * FROM b2badminuser.SupplierOrderDetail a WITH (NOLOCK)
	WHERE a.SODOrderNo = 40085193	

	SELECT * FROM b2badminuser.SupplierOrderOtherCharge a WITH (NOLOCK)
	WHERE a.OtherChgSupOrderID = 
	(
		SELECT SODSupOrderID FROM b2badminuser.SupplierOrderDetail x
		WHERE x.SODOrderNo = 40085193	
	)
		

