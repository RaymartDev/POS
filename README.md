# P.O.S (Point Sale System)

P.O.S System is one of the ruling software that businesses are using. One of the focal objectives of this software is to let the business owner track the sales, flow of inventory/stocks, and other information that will help the business owner not to do these things manually.

The sole purpose of this is to document the software that I developed for the client.

## Client’s Request

- The client wanted to have an inventory system where he can track the numbers of stocks from the commissary and to his outlet
- Wanted to see the number of sales per day, per year, and custom dates that he wanted
- Wanted to have one software for admin and cashiers
- Wanted to see every cashier's details such as sales per day, how much sales, and most important how many stocks are used with every transaction.

## Software Features

- SQL Supported (Microsoft SQL Server)
- Configurable (Config file is available to change your connection settings to SQL)
- Password is hashed (Secured)
- Secured for potential SQL Injection
- Account for user and admin are fully controlled by super admin

### Note:

The client does not want me to spend much time with the design. All the designs you are going to see are simple because I am more focused with the functionality of the project.

## Login Form

![Image](../images/image1.png)

Depending on the role it will automatically log you on to your respective tools.

## Admin / Super Admin

### Note:

Admin and Super Admin access are all the same except that Super Admin is the only one who can access the commissary inventory.

## Dashboard

![Image](../images/image2.png)

### Function:

- This will show you the total sales in a day which is the day you are looking the dashboard.
- This will show you the products count that are available to sell.
- This will show you the total stocks we have in this outlet.
- This will show you how many critical items or the items that needs to be re stocked.
- This will also show you the chart of sales in a yearly basis.
- You will get a notification if the product needs to be re stocked which will show at the bottom right of your screen and this is the sample:

![Image](../images/image3.png)

## Vendor

![Image](../images/image4.png)

### Function:

- This will show you all the available vendors.
- You can add vendors by typing their information.
- You can edit a vendor.
- You can delete a vendor.

## Manage Product

![Image](../images/image5.png)

### Function:

- The admin doesn’t have much functionality here because all the products that will show here are from the commissary records that is accessible only by Super Administrator.
- In this form it will only show you all the lists of the product and you are also allowed to search if there is a lot.
- Re–Order means it is the level or if the stocks hit that quantity or below it will be considered critical item or an item that needs a stock. Which will show in the dashboard or notifications.

## Stock Entry

![Image](../images/image6.png)

### Function:

- You can add stocks here from outlet.
- You can select a vendor where the stock will come from.
- You must generate a reference # about this stock event so that it will be shown in Stock In History after you finished stocking in.

## Stock History

![Image](../images/image7.png)

### Function:

- You can check here the records of the stock in that happened from outlet.
- You can check the reference #, description of the product, quantity and who made that process.
- You can also modify the date range above.

## Stock Adjustment

![Image](../images/image8.png)

### Function:

- You can select all one of the available item then choose a command and put a remarks to complete an adjustment.
- There are three available commands: Add to inventory ( By doing this you will add quantity to that product as an administrator ), Remove from inventory ( If by chance there is a mistake in stock in, you can remove quantity of product as an administrator ), Outlet to Commissary ( You can remove quantity from outlet and add stock to the commissary inventory )
- You can search a product by its description if there is a lot.

## Records

![Image](../images/image9.png)

### Function:

- There are varieties of options: Top Selling, Sold Items, Critical Stocks, Inventory List, Cancelled Order, Stock In History. Which will allow you to see all the data sets information about the specific category you want and also has an option to change the date range.

## Sales History

![Image](../images/image10.png)

### Function

- Here you can see all the data and also by filtering it by date range and cashier or all of your cashier.
- The total is responsive to the total amount of the sets of data you are looking at.
- The x button at the end will allow you to cancel an item. And will also show in cancelled item history.

## Inventory

![Image](../images/image11.png)

### Note:

This is only available to super admin

### Function:

- You can add product and put their quantity.
- You can edit existing product and delete it.
- This information will appear to manage product which is available for administrators.

## User Settings

### Note:

This is only available to super admin

### Function:

- You can create any account here: Super Admin, Admin or Cashier.
- You can change anyone’s password including your password.
- You can delete any account, activate or deactivate an account.
