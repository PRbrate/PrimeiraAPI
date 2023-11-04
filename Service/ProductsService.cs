using OfficeOpenXml;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoriesService _categoriesService;
        public ProductsService(IProductRepository productRepository, ICategoriesService categoriesService)
        {
            _ProductRepository = productRepository;
            _categoriesService = categoriesService;
        }
        public async Task<Product> Create(Product product)
        {
            return await _ProductRepository.Create(product);
        }

        public async Task Delete(Product product)
        {
            await _ProductRepository.Delete(product);
        }

        public async Task<ResponseBase<ProductDTO>> GetDepartaments()
        {
            return await _ProductRepository.GetDepartaments();
        }

        public async Task<Product> GetProcuctById(int id)
        {
            return await _ProductRepository.GetProcuctById(id);
        }
        public async Task<Product> Update(Product product)
        {
            return await _ProductRepository.Update(product);
        }

        public async Task<List<Product>> ImportExcel(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var listProduct = new List<Product>();

                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    var totalRows = worksheet.Dimension.End.Row;
                    var totalCollumns = worksheet.Dimension.End.Column;

                    for (int j = 4; j < totalRows; j++)
                    {
                        var cat = await _categoriesService.GetCategory();

                        List<Category> categories = cat.Items.ToList();

                        Product products = new Product();

                        products.Name = worksheet.Cells[j, 1].Value.ToString();
                        products.Description = worksheet.Cells[j, 2].Value.ToString();
                        products.Value = Convert.ToDecimal(worksheet.Cells[j, 3].Value);
                        var NameCategory = worksheet.Cells[j, 4].Value.ToString();

                        bool categoryExistis = categories.Where(c => c.Name.ToLower() == NameCategory.ToLower()).Any();



                        if (categoryExistis == false)
                        {
                            var category = new Category()
                            {
                                Name = NameCategory,
                            };
                            await _categoriesService.Create(category);

                        }
                        var item = cat.Items.Where(c => c.Name.ToLower() == NameCategory.ToLower()).FirstOrDefault();
                        products.CategoryId = item.Id;
                        listProduct.Add(products);
                    }
                    return listProduct;
                }
            }
        }

        public async Task<IEnumerable<Product>> SaveExcel(Task<List<Product>> products)
        {
            foreach (Product product in await products)
            {
                await Create(product);
            }
            return await products;
        }
    }
}