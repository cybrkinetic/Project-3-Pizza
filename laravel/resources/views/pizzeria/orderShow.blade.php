<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Uw Bestelling</title>
    <link rel="apple-touch-icon" sizes="180x180" href="img/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="img/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="img/favicon-16x16.png">
    <link rel="manifest" href="/site.webmanifest">
</head>

<body>
    @include('pizzeria.header')

    <div
        class="grid w-full grid-cols-1 p-4 sm:grid-cols-[0.8fr,6.5fr,0.8fr] md:grid-cols-[0.6fr,7fr,0.6fr] lg:grid-cols-[0.8fr,7fr,0.8fr] 2xl:grid-cols-[1.3fr,5fr,1.3fr] gap-4 md:gap-0 min-h-screen">
        <!-- Linkse kolom -->
        <div class="hidden sm:block"></div>

        <!-- Middel kolom -->
        <div class="flex flex-col space-y-8">
            <!-- titel -->
            <h1 class="text-4xl sm:text-4xl xl:text-5xl text-[#483F3F] font-koulen">Uw Bestelling</h1>

            <!-- Pizza's -->
            <div class="grid grid-cols-1 gap-4 px-4">
                {{-- $besteldePizza->PizzaNaam->id --}}
                @foreach ($besteldePizzas as $besteldePizza)
                <?php
                    $prijs = $besteldePizza->PizzaNaam->pizzaPrijs;
                    $multiply = $besteldePizza->PizzaSize->priceMultiplyer;
                    $pizzaPrijs = $prijs * $multiply;
                    $formattedPrijs = number_format((float) $pizzaPrijs, 2, ',', '');
                    ?>
                <div class="bg-[#666060] p-4 rounded-md shadow-lg">
                    <div class="flex items-center space-x-4">
                        <div class="h-28 bg-[#7B7373] rounded w-96">
                            <img src="{{ asset('img/' . strtolower(str_replace(' ', '-', $besteldePizza->PizzaNaam->pizzaNaam)) . '.jpg') }}"
                                alt="{{ $besteldePizza->PizzaNaam->pizzaNaam }}"
                                class="w-96 h-full object-cover rounded">
                        </div>
                        <div>
                            <p class="text-2xl text-white">{{ $besteldePizza->PizzaNaam->pizzaNaam }}</p>
                            <p class="text-xl text-[#D9D9D9]">{{ Str::title($besteldePizza->PizzaSize->grootte) }}</p>
                            <p class="text-xl text-[#D9D9D9]">€{{ $formattedPrijs }}</p>
                            <p class="text-xl text-[#D9D9D9]">Status: {{ $besteldePizza->PizzaStatus->status}}</p>
                        </div>
                    </div>
                </div>
                @endforeach

                <!-- Totale prijs en status -->
                <div class="bg-[#E8C63F] font-koulen text-2xl text-[#483F3F] p-4 rounded-md shadow-lg">
                    <?php
                    $formattedTotalPrijs = number_format((float) $totaalPrijs, 2, ',', '');
                    ?>
                    <p>Totaal Prijs: &euro;{{ $formattedTotalPrijs }}</p>
                    <p>Order Status: {{ $order->OrderStatus->status }}</p>
                </div>
            </div>
        </div>

        <!-- Rechter kolom -->
        <div class="hidden sm:block"></div>
    </div>

    @include('pizzeria.footer')
</body>

</html>