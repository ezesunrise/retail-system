using AutoMapper;
using RetailSystem.Dtos;
using RetailSystem.Models;

namespace RetailSystem.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(u => u.NormalizedUserName, opt => opt.MapFrom(u => u.UserName.ToUpper()))
                .ForMember(u => u.NormalizedEmail, opt => opt.MapFrom(u => u.Email.ToUpper()));

            CreateMap<Business, BusinessDto>();
            CreateMap<BusinessDto, Business>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();

            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();
            
            CreateMap<PurchaseOrder, PurchaseOrderDto>();
            CreateMap<PurchaseOrderDto, PurchaseOrder>();

            CreateMap<Receipt, ReceiptDto>();
            CreateMap<ReceiptDto, Receipt>();

            CreateMap<ReportGroup, ReportGroupDto>();
            CreateMap<ReportGroupDto, ReportGroup>();

            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>()
                .ForMember(s => s.ReferenceNumber, opt => opt.Ignore());

            CreateMap<SubCategory, SubCategoryDto>();
            CreateMap<SubCategoryDto, SubCategory>();

            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();

            CreateMap<Supply, SupplyDto>();
            CreateMap<SupplyDto, Supply>();

            CreateMap<Transfer, TransferDto>();
            CreateMap<TransferDto, Transfer>();

            CreateMap<Unit, UnitDto>();
            CreateMap<UnitDto, Unit>();

            CreateMap<TransferItem, TransferItemDto>();
            CreateMap<TransferItemDto, TransferItem>()
                .ForMember(s => s.TransferId, ops => ops.Ignore());

            CreateMap<SupplyItem, SupplyItemDto>();
            CreateMap<SupplyItemDto, SupplyItem>()
                .ForMember(s => s.SupplyId, ops => ops.Ignore());

            CreateMap<SaleItem, SaleItemDto>()
                .ForMember(s => s.ItemDescription, opt => opt.MapFrom(s => s.Item.Description));
            CreateMap<SaleItemDto, SaleItem>()
                .ForMember(s => s.SaleId, ops => ops.Ignore());

            CreateMap<ReportItem, ReportItemDto>();
            CreateMap<ReportItemDto, ReportItem>()
                .ForMember(r => r.ReportGroupId, ops => ops.Ignore());

            CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>();
            CreateMap<PurchaseOrderItemDto, PurchaseOrderItem>()
                .ForMember(p => p.PurchaseOrderId, ops => ops.Ignore());
            
            CreateMap<LocationItem, LocationItemDto>();
            CreateMap<LocationItemDto, LocationItem>();

            CreateMap<InvoiceItem, InvoiceItemDto>();
            CreateMap<InvoiceItemDto, InvoiceItem>()
                .ForMember(i => i.InvoiceId, ops => ops.Ignore());

            //Lists

            CreateMap<AppUser, AppUserListDto>();
            CreateMap<AppUserListDto, AppUser>();

            CreateMap<Business, BusinessListDto>();
            CreateMap<BusinessListDto, Business>();

            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryListDto, Category>();

            CreateMap<Customer, CustomerListDto>();
            CreateMap<CustomerListDto, Customer>();

            CreateMap<Invoice, InvoiceListDto>();
            CreateMap<InvoiceListDto, Invoice>();

            CreateMap<Item, ItemListDto>();
            CreateMap<ItemListDto, Item>();

            CreateMap<Location, LocationListDto>();
            CreateMap<LocationListDto, Location>();

            CreateMap<LocationItem, LocationItemListDto>();
            CreateMap<LocationItemListDto, LocationItem>();

            CreateMap<Manufacturer, ManufacturerListDto>();
            CreateMap<ManufacturerListDto, Manufacturer>();

            CreateMap<PurchaseOrder, PurchaseOrderListDto>();
            CreateMap<PurchaseOrderListDto, PurchaseOrder>();

            CreateMap<Sale, SaleListDto>();
            CreateMap<SaleListDto, Sale>();

            CreateMap<SubCategory, SubCategoryListDto>();
            CreateMap<SubCategoryListDto, SubCategory>();

            CreateMap<Supplier, SupplierListDto>();
            CreateMap<SupplierListDto, Supplier>();

            CreateMap<Supply, SupplyListDto>();
            CreateMap<SupplyListDto, Supply>();

            CreateMap<Transfer, TransferListDto>();
            CreateMap<TransferListDto, Transfer>();

            CreateMap<Unit, UnitListDto>();
            CreateMap<UnitListDto, Unit>();

        }
    }
}
