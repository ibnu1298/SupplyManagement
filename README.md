## **🗄️ Database Migration & Seeding**

### **Adding a New Migration**

1. Open **Visual Studio**.
2. Set **"SupplyManagement.API"** as the **Startup Project**.
3. Open **Package Manager Console**.
4. Run the following command:
   ```
   Add-Migration -s SupplyManagement.API -p SupplyManagement.DataAccess -c ApplicationDbContext
   ```
5. Run the following command:
   ```
   Update-Database
   ```
