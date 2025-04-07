using AutoMapper;
using StoreNet.API.Dtos.Address;
using StoreNet.API.Dtos.Authentication;
using StoreNet.API.Dtos.Authentication.Token;
using StoreNet.API.Dtos.Brand;
using StoreNet.API.Dtos.Cart;
using StoreNet.API.Dtos.Cart.CartItems;
using StoreNet.API.Dtos.Category;
using StoreNet.API.Dtos.Order;
using StoreNet.API.Dtos.Order.OrderItems;
using StoreNet.API.Dtos.Product;
using StoreNet.API.Dtos.User;
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

namespace StoreNet.API.Mapping;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // PRODUCT
        CreateMap<ProductDto, ProductResponse>().ReverseMap();
        CreateMap<CreateProductRequest, ProductCreateDto>();
        CreateMap<(UpdateProductRequest Request, Guid Id), UpdateProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            

        // USER
        CreateMap<UserDto, UserResponse>().ReverseMap();
        CreateMap<CreateUserRequest, RegisterDto>();
        CreateMap<UpdateUserRequest, UpdateUserDto>();

        // AUTH
        CreateMap<RegisterRequest, RegisterDto>();
        CreateMap<RefreshTokenRequest, RefreshTokenDto>();
        CreateMap<LoginRequest,LoginDto>();

        // ADDRESS
        CreateMap<AddressDto, AddressResponse>().ReverseMap();
        CreateMap<CreateAddressRequest, CreateAddressDto>();
        CreateMap<UpdateAddressRequest, UpdateAddressDto>();

        // ORDER
        CreateMap<OrderDto, OrderResponse>().ReverseMap();
        CreateMap<CreateOrderRequest, CreateOrderDto>();
        CreateMap<UpdateOrderRequest, UpdateOrderDto>();

        // ORDER ITEM
        CreateMap<OrderItemDto, OrderItemResponse>().ReverseMap();
        CreateMap<AddOrderItemRequest, AddOrderItemDto>();

        // CART
        CreateMap<CartDto, CartResponse>()
            .ReverseMap();

        // CART ITEM
        CreateMap<CartItemDto, CartItemResponse>().ReverseMap();
        CreateMap<AddCartItemRequest, AddCartItemDto>().ReverseMap();
        CreateMap<UpdateCartItemRequest, UpdateCartItemDto>().ReverseMap();

        // BRAND
        CreateMap<BrandDto, BrandResponse>().ReverseMap();
        CreateMap<CreateBrandRequest, CreateBrandDto>();
        CreateMap<UpdateBrandRequest, UpdateBrandDto>();

        // CATEGORY
        CreateMap<CategoryDto, CategoryResponse>().ReverseMap();
        CreateMap<CreateCategoryRequest, CreateCategoryDto>();
        CreateMap<UpdateCategoryRequest, UpdateCategoryDto>();


    }
}

