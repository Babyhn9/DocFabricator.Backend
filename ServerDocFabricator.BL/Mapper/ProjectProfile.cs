

using ServerDocFabricator.BL.DTO;
using AutoMapper;
using ServerDocFabricator.DAL.Entities;
namespace ServerDocFabricator.BL.Mapper;



public class ProjectProfile : Profile
{

    public ProjectProfile()
    {
        Init();
    }

    private void Init()
    {
        CreateMap<TemplateEntity, DisplayTemplateDto>();
        CreateMap<TemplateEntity, TemplateDto>();
    }
}