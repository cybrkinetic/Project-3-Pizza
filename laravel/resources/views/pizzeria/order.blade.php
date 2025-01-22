<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bestellen</title>
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

            <div class="container mx-auto p-6">
                <h1 class="mt-10 font-koulen text-4xl sm:text-4xl xl:text-5xl text-[#483F3F]">Jouw Bestelling</h1>
                <div class="flex flex-col md:flex-row justify-between space-x-0 md:space-x-4">

                    <!-- Bestelde pizza's -->
                    <div class="flex-1 space-y-4">
                        @foreach ($besteldePizzas as $besteldePizza)
                        <!-- Je bestelling -->
                        <?php
            $prijs = $besteldePizza->PizzaNaam->pizzaPrijs;
            $multiply = $besteldePizza->PizzaSize->priceMultiplyer;
            $pizzaPrijs = $prijs * $multiply;
            $formattedPrijs = number_format((float) $pizzaPrijs, 2, ',', '');
            ?>

                        <div class="flex justify-between items-center bg-[#666060] text-white rounded-md p-4 shadow-md">
                            <!-- Pizza Info -->
                            <div class="flex items-center space-x-4">
                                <div class="h-28 bg-[#7B7373] rounded w-96">
                                    <img src="{{ asset('img/' . strtolower(str_replace(' ', '-', $besteldePizza->PizzaNaam->pizzaNaam)) . '.jpg') }}"
                                        alt="{{ $besteldePizza->PizzaNaam->pizzaNaam }}"
                                        class="w-96 h-full object-cover rounded">
                                </div>
                                <div>
                                    <p class="text-2xl">{{ $besteldePizza->PizzaNaam->pizzaNaam }}</p>
                                    <p class="text-xl text-[#D9D9D9]">
                                        {{ Str::title($besteldePizza->PizzaSize->grootte) }}</p>
                                    <p class="text-xl text-[#D9D9D9]">€{{ $formattedPrijs }}</p>
                                </div>
                            </div>
                            <!-- Verwijder knop -->
                            <div class="flex items-center">
                                <input type="hidden" name="PizzaID" value="{{ $besteldePizza->id }}">
                                <form class="pizzalist" action="{{ route('order.destroy', $besteldePizza->id) }}"
                                    method="POST">
                                    @csrf
                                    @method('DELETE')
                                    <button type="submit"
                                        class="text-[#72C35C] underline hover:text-[#61A84E]">Verwijderen</button>
                                </form>
                            </div>
                        </div>
                        @endforeach
                    </div>

                    <!-- Bestelling info -->
                    <div class="w-full md:w-1/3">
                        <div class="bg-[#666060] text-white rounded-md p-4">
                            <form action="{{ route('orderlist.store') }}" method="POST">
                                @csrf
                                @method('POST')
                                <?php 
            $formattedTotalPrijs = number_format((float) $totaalPrijs, 2, ',', '');?>
                                <div
                                    class="bg-[#E8C63F] text-[#483F3F] font-koulen text-2xl px-4 py-2 rounded mb-4 w-full text-center">
                                    Vestiging: Eindhoven</div>
                                <div class="mb-4 flex justify-between">
                                    <p class="text-xl font-bold">Totaal</p>
                                    <p class="text-xl">€{{ $formattedTotalPrijs }}</p>
                                </div>
                                <input type="submit"
                                    class="bg-[#72C35C] text-white px-6 py-3 rounded w-full font-koulen text-xl"
                                    value="Afrekenen"></input>
                            </form>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <!-- Rechter kolom -->
        <div class="hidden sm:block"></div>
    </div>
    <script src="js/cart.js"></script>
    @include('pizzeria.footer')
</body>

</html>