﻿using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Wallet;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepo;
        private readonly IMapper _mapper;

        public WalletService(IWalletRepository walletRepo, IMapper mapper)
        {
            _mapper = mapper;
            _walletRepo = walletRepo;
        }
        public async Task Create(WalletCreateDto model)
        {
            await _walletRepo.Create(_mapper.Map<Wallet>(model));
        }

        public async Task<WalletDto> GetUserBalance(string userId)
        {
            var userBalance = await _walletRepo.GetUserBalance(userId);
            decimal balance = userBalance.Sum(m => m.Balance);
            return new WalletDto { Balance = balance };
        }
    }
}
