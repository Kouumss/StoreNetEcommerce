using AutoMapper;
using StoreNet.Application.Dtos.Addresses;
using StoreNet.Application.Dtos.Auth;
using StoreNet.Application.Dtos.Brands;
using StoreNet.Application.Dtos.Carts;
using StoreNet.Application.Dtos.Carts.CartItems;
using StoreNet.Application.Dtos.Category;
using StoreNet.Application.Dtos.Orders;
using StoreNet.Application.Dtos.Orders.OrderItems;
using StoreNet.Application.Dtos.Products;
using StoreNet.Application.Dtos.Users;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Mapping;
public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        // PRODUCT
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        // USER
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<UpdateUserDto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src));
        CreateMap<CreateUserDto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        // AUTH
        CreateMap<RegisterDto, AppUser>();

        // ADDRESS
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<CreateAddressDto, Address>();
        CreateMap<UpdateAddressDto, Address>();

        // ORDER
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();

        // ORDER ITEM
        CreateMap<OrderItem, OrderItemDto>().ReverseMap()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));
        CreateMap<AddOrderItemDto, OrderItem>();

        // CART
        // Mapping Cart (Entity) → CartDto
        CreateMap<Cart, CartDto>().ReverseMap();


        // CART ITEM
        CreateMap<CartItem, CartItemDto>()
          .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product ?? null));

        CreateMap<CartItemDto, CartItem>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<AddCartItemDto, CartItem>().ReverseMap();
        CreateMap<UpdateCartItemDto, CartItem>().ReverseMap();

        // BRAND
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();

        // CATEGORY
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

    }
}

