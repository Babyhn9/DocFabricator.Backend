

using ServerDocFabricator.BL.DTO;
using ServerDocFabricator.DAL;

namespace ServerDocFabricator.BL.Mapper;

using AutoMapper;
using ServerDocFabricator.DAL.Entities;

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