using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Shared.Constants;
using ECommerce.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Templates
{
    public class EmailTemplates : IEmailTemplates
    {
        private readonly IEmailService _emailService;
        private readonly IRepository<UserAggregate, User> _userRepository;
        private readonly IRepository<OrderAggregate, Order> _orderRepository;
        private readonly IMapper _mapper;

        public EmailTemplates(IEmailService emailService, IRepository<UserAggregate, User> userRepository, IRepository<OrderAggregate, Order> orderRepository, IMapper mapper)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task SendVerificationEmailAsync(Guid userId)
        {
            // Get User
            var user = await GetUserByIdAsync(userId);

            var verificationLink = $"https://{Constants.MyIpv4}/api/v1/Auth/verify-email?token={Uri.EscapeDataString(user.EmailVerificationToken!)}";

            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Email Verification Required",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>Thank you for registering with us. To complete your registration, please verify your email address by clicking the link below:</p>
                        <p><a href='{verificationLink}' target='_blank'>Verify Email Address</a></p>
                        <p>If you did not request this verification, please ignore this email.</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true,
                Link = verificationLink
            };

            await _emailService.SendEmailAsync(dto);
        }

        public async Task SendOrderEmailAsync(Guid userId, Guid orderId, eEventType eventType)
        {
            // Get User
            var user = await GetUserByIdAsync(userId);

            // Get User
            var order = await GetOrderByIdAsync(orderId);

            switch (eventType)
            {
                case eEventType.OrderPlaced:
                    await SendOrderPlacedEmailAsync(user, order);
                    break;

                case eEventType.OrderShipped:
                    await SendOrderShippedEmailAsync(user, order);
                    break;

                case eEventType.OrderDelivered:
                    await SendOrderDeliveredEmailAsync(user, order);
                    break;

                case eEventType.OrderCanceled:
                    await SendOrderCanceledEmailAsync(user, order);
                    break;
            }
        }

        private async Task SendOrderPlacedEmailAsync(UserDTO user, OrderDTO order)
        {
            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Order Confirmation - ECommerce Pvt Ltd",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>Thank you for shopping with us! We are pleased to inform you that your order has been placed successfully. Below are the details of your order:</p>
                        <p>Order Date: <b>{order.OrderPlacedDate:MMMM dd, yyyy HH:mm}</b></p>
                        <p>Total Amount: <b>{order.TotalAmount.Amount}</b></p>
                        <p>Your order number is <b>{order.Id}</b>.</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true
            };

            await _emailService.SendEmailAsync(dto);
        }

        private async Task SendOrderShippedEmailAsync(UserDTO user, OrderDTO order)
        {
            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Your Order Has Been Shipped - ECommerce Pvt Ltd",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>Good news! Your order has been shipped and is on its way to you. Below are the details of your order:</p>
                        <p>Order Date: <b>{order.OrderShippedDate:MMMM dd, yyyy HH:mm}</b></p>
                        <p>Total Amount: <b>{order.TotalAmount.Amount}</b></p>
                        <p>Your order number is <b>{order.Id}</b>.</p>
                        <br/>
                        <p>Thank you for choosing ECommerce Pvt Ltd!</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true
            };

            await _emailService.SendEmailAsync(dto);
        }

        private async Task SendOrderDeliveredEmailAsync(UserDTO user, OrderDTO order)
        {
            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Your Order Has Been Delivered - ECommerce Pvt Ltd",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>We are happy to inform you that your order has been successfully delivered. Below are the details of your order:</p>
                        <p>Order Date: <b>{order.OrderDeliveredDate:MMMM dd, yyyy HH:mm}</b></p>
                        <p>Total Amount: <b>{order.TotalAmount.Amount}</b></p>
                        <p>Your order number is <b>{order.Id}</b>.</p>
                        <p>We hope you enjoy your purchase. If you have any questions or feedback, feel free to contact us at support@example.com.</p>
                        <br/>
                        <p>Thank you for choosing ECommerce Pvt Ltd!</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true
            };

            await _emailService.SendEmailAsync(dto);
        }

        private async Task SendOrderCanceledEmailAsync(UserDTO user, OrderDTO order)
        {
            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Your Order Has Been Canceled - ECommerce Pvt Ltd",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>We regret to inform you that your order has been canceled. Below are the details of your order:</p>
                        <p>Order Date: <b>{order.OrderCanceledDate:MMMM dd, yyyy HH:mm}</b></p>
                        <p>Total Amount: <b>{order.TotalAmount.Amount}</b></p>
                        <p>Your order number was <b>{order.Id}</b>.</p>
                        <p>If you have any questions regarding this cancellation, please reach out to us at support@example.com.</p>
                        <br/>
                        <p>We apologize for any inconvenience caused.</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true
            };

            await _emailService.SendEmailAsync(dto);
        }

        private async Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetDbSet()
                .SingleOrDefaultAsync(x => x.Id == userId);

            return _mapper.Map<UserDTO>(user);
        }

        private async Task<OrderDTO> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetDbSet()
                .SingleOrDefaultAsync(x => x.Id == orderId);

            return _mapper.Map<OrderDTO>(order);
        }
    }
}