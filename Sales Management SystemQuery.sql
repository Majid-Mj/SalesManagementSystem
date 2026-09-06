
CREATE TABLE "Users"
(
    "Id" SERIAL PRIMARY KEY,
    "UserName" VARCHAR(100) NOT NULL UNIQUE,
    "PasswordHash" VARCHAR(255) NOT NULL
);

CREATE TABLE "Products"
(
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Code" VARCHAR(50) UNIQUE NOT NULL,
    "Cost" DECIMAL(18,2) NOT NULL CHECK ("Cost" > 0),
    "Price" DECIMAL(18,2) NOT NULL CHECK ("Price" > 0)
);

CREATE TABLE "Customers"
(
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Code" VARCHAR(50) UNIQUE NOT NULL,
    "Address" VARCHAR(255)
);

CREATE TABLE "SalesInvoiceMaster"
(
    "Id" SERIAL PRIMARY KEY,
    "InvoiceNumber" VARCHAR(50) NOT NULL,
    "InvoiceDate" TIMESTAMP NOT NULL,
    "CustomerId" INT NOT NULL,
    "TotalAmount" DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT "FK_SalesInvoiceMaster_Customers"
        FOREIGN KEY ("CustomerId")
        REFERENCES "Customers"("Id")
);

CREATE TABLE "SalesInvoiceDetails"
(
    "Id" SERIAL PRIMARY KEY,
    "InvoiceId" INT NOT NULL,
    "ProductId" INT NOT NULL,
    "Quantity" INT NOT NULL CHECK ("Quantity" > 0),
    "Rate" DECIMAL(18,2) NOT NULL CHECK ("Rate" > 0),
    "Amount" DECIMAL(18,2) NOT NULL,

    CONSTRAINT "FK_SalesInvoiceDetails_Master"
        FOREIGN KEY ("InvoiceId")
        REFERENCES "SalesInvoiceMaster"("Id"),

    CONSTRAINT "FK_SalesInvoiceDetails_Product"
        FOREIGN KEY ("ProductId")
        REFERENCES "Products"("Id")
);



CREATE  FUNCTION sp_get_all_products()
RETURNS TABLE (
    "Id" INT,
    "Name" VARCHAR(100),
    "Code" VARCHAR(50),
    "Cost" NUMERIC,
    "Price" NUMERIC
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT p."Id", p."Name", p."Code", p."Cost", p."Price"
    FROM "Products" p;
END;
$$;




CREATE FUNCTION sp_get_product_by_id(p_id INT)
RETURNS TABLE (
    "Id" INT,
    "Name" VARCHAR(100),
    "Code" VARCHAR(50),
    "Cost" NUMERIC,
    "Price" NUMERIC
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT p."Id", p."Name", p."Code", p."Cost", p."Price"
    FROM "Products" p
    WHERE p."Id" = p_id;
END;
$$;



CREATE FUNCTION "sp_get_all_invoices"()
RETURNS TABLE (
    "Id" INT,
    "InvoiceNumber" VARCHAR(50),
    "InvoiceDate" TIMESTAMP,
    "CustomerId" INT,
    "TotalAmount" DECIMAL(18,2)
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        i."Id", 
        i."InvoiceNumber", 
        i."InvoiceDate", 
        i."CustomerId", 
        i."TotalAmount"
    FROM "SalesInvoiceMaster" i;
END;
$$;


CREATE FUNCTION "sp_get_invoice_by_id"(p_id INT)
RETURNS TABLE (
    "Id" INT,
    "InvoiceNumber" VARCHAR(50),
    "InvoiceDate" TIMESTAMP,
    "CustomerId" INT,
    "TotalAmount" DECIMAL(18,2)
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        i."Id", 
        i."InvoiceNumber", 
        i."InvoiceDate", 
        i."CustomerId", 
        i."TotalAmount"
    FROM "SalesInvoiceMaster" i
    WHERE i."Id" = p_id;
END;
$$;




