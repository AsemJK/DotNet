// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorTicketSystem.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using BlazorTicketSystem.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\_Imports.razor"
using BlazorTicketSystem.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\Pages\Earnings.razor"
using BlazorTicketSystem.Client.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\Pages\Earnings.razor"
using BlazorTicketSystem.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\Pages\Earnings.razor"
using BlazorTicketSystem.Shared.ViewModels;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/earnings")]
    public partial class Earnings : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 51 "C:\Code\Test\BlazorTicketSystem\BlazorTicketSystem\Client\Pages\Earnings.razor"
       
    private List<Earning> earnings;
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        earnings = await Http.GetFromJsonAsync<List<Earning>>("api/Earnings");
        StateHasChanged();
    }
    public async void Refresh()
    {
        await LoadData();
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
