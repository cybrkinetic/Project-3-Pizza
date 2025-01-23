<!DOCTYPE html>

<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/css2?family=Koulen&display=swap" rel="stylesheet">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <title>Header</title>
    @vite('resources/css/app.css')
</head>

<body>

    <header class="border-b-2 border-[#483F3F] p-4 flex flex-col sm:flex-row sm:justify-between sm:items-center">
        <!-- Site logo -->
        <div class="flex justify-center sm:justify-between items-center">
            <a href="/">
                <img src="/img/StonksPizzaLOGO.png" class="h-30 w-60 mt-1" href="#">
            </a>
            <!-- Hamburger Icoontje -->
            <button id="menu-toggle" class="sm:hidden text-black p-0 focus:outline-none">
                <svg class="w-6 h-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                    stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16m-7 6h7" />
                </svg>
            </button>
        </div>
        <i data-fa-symbol="" class="fa-solid fa-magnifying-glass fa-sm"></i>
        <!-- Nav menu -->
        <nav id="menu" class="hidden sm:flex sm:space-x-4">
            <ul class="flex flex-col sm:flex-row space-y-2 sm:space-y-0 sm:space-x-4">
                <li><a href="/menu"
                        class="text-2xl font-koulen text-[#483F3F] mt-2 lg:mr-2 {{ request()->is('menu') ? 'bg-[#D9D9D9] rounded px-2' : '' }}">Menu</a>
                </li>
                @guest <li><a href="/register" class="text-2xl font-koulen text-[#483F3F] mt-2 lg:mr-2">Registreren</a>
                </li>
                <li><a href="/login" class="text-2xl font-koulen text-[#483F3F] mt-2 lg:mr-2">Inloggen</a></li>@endguest
                @auth
                <li><a href="/orderlist"
                        class="text-2xl font-koulen text-[#483F3F] mt-2 lg:mr-2 {{ request()->is('orderlist') ? 'bg-[#D9D9D9] rounded px-2' : '' }}">Bestellingen</a>
                </li>
                <li><a href="/dashboard" class="text-2xl font-koulen text-[#483F3F] mt-2 lg:mr-2">Account</a></li>
                @endauth
                <li><a href="/search" class="text-2xl font-koulen text-[#483F3F] mt-2"><img
                            src="/img/magnifying-glass-solid.svg" class="h-5 w-auto mt-1 lg:mr-2"></img></a></li>
                <li><a href=" {{route('order.index')}}" class="text-2xl font-koulen text-[#483F3F] mt-2"><img
                            src="/img/cart-shopping-solid.svg"
                            class="h-5 w-auto mt-1 lg:mr-10 {{ request()->is('order') ? 'bg-[#D9D9D9] rounded px-4 pt-1 pb-1 h-8 -mt-0.5 ' : '' }}"></img>
                            @if(isset($cartItemCount) && $cartItemCount > 0)
        <span class="bg-[#E8C63F] text-[#483F3F] text-xs rounded-full px-2 absolute -mt-7 ml-3">
            {{ $cartItemCount }}
        </span>
        @endif
                    </a></li>
            </ul>
        </nav>
    </header>
    <!-- Header script -->
    <script src="/js/header.js"></script>
</body>

</html>