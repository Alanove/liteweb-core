namespace lw.Domain.Services;

public interface IMenuService
{
	Menu AddMenu(Menu menu);
	Menu? CurrentMenu();
    IEnumerable<Menu> AllMenus();
}
