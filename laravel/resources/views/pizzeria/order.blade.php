<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="apple-touch-icon" sizes="180x180" href="img/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="img/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="img/favicon-16x16.png">
    <link rel="manifest" href="/site.webmanifest">
</head>

<body>
    @include('pizzeria.header')

    <div
        class="grid w-full grid-cols-1 p-4 sm:grid-cols-[0.8fr,6.5fr,2.5fr,0.8fr] md:grid-cols-[0.6fr,7fr,3fr,0.6fr] lg:grid-cols-[0.8fr,7fr,3fr,0.8fr] 2xl:grid-cols-[1.3fr,5fr,2fr,1.3fr] gap-4 md:gap-0 min-h-screen">
        <!-- Linkse kolom -->
        <div class="hidden sm:block"></div>

        <!-- Middel kolom -->
        <div class="flex flex-col space-y-8">
            <h1 class="mt-10 font-koulen text-2xl text-[#483F3F]">Jouw Bestelling</h1>
            <ol>

                <!--   <div class="pizzalist">
                    @foreach ($besteldePizzas as $besteldePizza)
                    <?php
            $prijs = $besteldePizza->PizzaNaam->pizzaPrijs;
            $multiply = $besteldePizza->PizzaSize->priceMultiplyer;
            $pizzaPrijs = $prijs * $multiply;
            $formattedPrijs = number_format((float) $pizzaPrijs, 2, ',', '');
            ?>
                    <div class='pizza'>
                        <div class="bg-[#666060] rounded-md shadow-lg">
                            <div class="h-28 bg-[#7B7373] rounded ml-3 mr-3 mt-3 mb-5">
                                <img src="{{ asset('img/' . strtolower(str_replace(' ', '-', $besteldePizza->PizzaNaam->pizzaNaam)) . '.jpg') }}"
                                    alt="{{ $besteldePizza->PizzaNaam->pizzaNaam }}"
                                    class="w-full h-full object-cover rounded"></img>


                                <p class="text-white text-l mb-1">{{ $besteldePizza->PizzaNaam->pizzaNaam }}</p>
                                {{-- $besteldePizza->PizzaNaam->id --}}
                                <p class="text-white text-l mb-1">{{ $besteldePizza->PizzaSize->grootte }}</p>
                                <p class="text-white text-l mb-3">€{{ $formattedPrijs }}</p>
                            </div>

                            <input type="hidden" name="PizzaID" value="{{ $besteldePizza->id }}">
                            <br />
                            <form class='pizzalist' action="{{ route('order.destroy', $besteldePizza->id) }}"
                                method="POST">
                                @csrf
                                @method('DELETE')
                                <button type="submit" class="btn btn-danger">Verwijder pizza</button>
                            </form>


                        </div>
                        @endforeach
                    </div>
                </div>
-->


                @foreach ($besteldePizzas as $besteldePizza)
                <!-- Je bestelling -->
                <div class="flex justify-between items-center bg-[#666060] text-white rounded-md p-4 mb-4 shadow-md">
                    <!-- Pizza Info-->
                    <div class="flex items-center space-x-4">
                        <div class="h-28 bg-[#7B7373] rounded w-96">
                            <img src="{{ asset('img/' . strtolower(str_replace(' ', '-', $besteldePizza->PizzaNaam->pizzaNaam)) . '.jpg') }}"
                                alt="{{ $besteldePizza->PizzaNaam->pizzaNaam }}"
                                class=" w-96 h-full object-cover rounded"></img>
                        </div>
                        <div class="">
                            <p class="text-2xl">{{$besteldePizza->PizzaNaam->pizzaNaam }}</p>
                            <p class="text-xl  text-[#D9D9D9]">{{Str::title($besteldePizza->PizzaSize->grootte)}}</p>
                            <p class="text-xl text-[#D9D9D9]">€{{$formattedPrijs}}</p>
                        </div>
                    </div>
                    <!-- Verwijder knop -->
                    <div class="flex items-center space-x-4">
                        <div class="flex items-center space-x-2">
                            <input type="hidden" name="PizzaID" value="{{ $besteldePizza->id }}">
                            <form class='pizzalist' action="{{ route('order.destroy', $besteldePizza->id) }}"
                                method="POST">
                                @csrf
                                @method('DELETE')
                                <button type="submit"
                                    class="text-[#72C35C] underline hover:text-[#61A84E]">Verwijderen</button>
                            </form>
                        </div>
                    </div>
                </div>
                @endforeach

                <!-- Bestelling overview vak -->
                <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 px-4">
                    <form class='bottomBtn' action="{{ route('orderlist.store') }}" method="POST">
                        @csrf

                        @method('POST')
                        <div class="confirmOrderBtn">
                            <input type="submit" id="send_order" value="plaats bestelling">
                            <?php
        $formattedTotalPrijs = number_format((float) $totaalPrijs, 2, ',', '');
        ?>
                            <p>Totaal Prijs: €{{ $formattedTotalPrijs }}</p>
                        </div>
                    </form>
                </div>
        </div>
        <!-- Rechter kolom -->
        <div class="hidden sm:block"></div>
    </div>
    <script src="js/cart.js"></script>

</body>

</html>