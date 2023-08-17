using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [ApiController]
    [Route("api/fornecedores")]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepo;
        private readonly IEnderecoRepository _enderecoRepo;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepo, IEnderecoRepository enderecoRepo, IMapper mapper, IFornecedorService fornecedorService, INotificador notificador) : base(notificador)
        {
            _fornecedorRepo = fornecedorRepo;
            _enderecoRepo = enderecoRepo;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            return CustomResponse(fornecedorViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
            {
                NotificarErro("O id informado nao e o mesmo");
                return CustomResponse(fornecedorViewModel);
            }

            if (!ModelState.IsValid)
                return CustomResponse(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            return CustomResponse(fornecedorViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var fornecedorViewModel = await ObterForncenedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            await _fornecedorService.Remover(id);

            return CustomResponse(fornecedorViewModel);
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepo.ObterTodos());

            return fornecedores;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = await _fornecedorRepo.ObterPorId(id);

            if (fornecedor == null)
                return NotFound();

            return _mapper.Map<FornecedorViewModel>(fornecedor);
        }

        [HttpGet("{id:guid}")]
        public async Task<FornecedorViewModel> ObterForncenedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepo.ObterFornecedorProdutosEndereco(id));
        }

        [HttpGet("{id:guid}")]
        public async Task<FornecedorViewModel> ObterForncenedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepo.ObterFornecedorEndereco(id));
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(await _enderecoRepo.ObterPorId(id));
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id)
            {
                NotificarErro("O id informado nao e o mesmo");
                return CustomResponse(enderecoViewModel);
            }

            if (!ModelState.IsValid)
                return CustomResponse(enderecoViewModel);

            var endereco = _mapper.Map<Endereco>(enderecoViewModel);
            await _enderecoRepo.Atualizar(endereco);

            return CustomResponse(enderecoViewModel);
        }
    }
}
