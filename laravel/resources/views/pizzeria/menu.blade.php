<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Stonks Pizza Menu</title>
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
        <h2 class="text-2xl sm:text-3xl xl:text-5xl text-[#483F3F] font-koulen">Ons menu</h2>
            
            <!-- Menu vakjes -->
            <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 px-4">
                @foreach($pizzalijst as $pizza)
                <!-- Menu item kaart -->
                <div class="bg-[#666060] rounded-md shadow-lg">
                    <div class="h-28 bg-[#7B7373] rounded ml-3 mr-3 mt-3">
                        <img src="{{ asset('img/' . strtolower(str_replace(' ', '-', $pizza->pizzaNaam)) . '.jpg') }}"
                            alt="{{ $pizza->pizzaNaam }}" class="w-full h-full object-cover rounded"></img>
                    </div>
                    <div class="p-4">
                        <form class="pizzabox" action="{{ route('menu.store') }}" method="POST">
                            @csrf
                            <p class="text-white text-l mb-1">{{ $pizza->pizzaNaam }}</p>

                            <input type="hidden" name="PizzaID" value="{{ $pizza->id }}">
                            @if (auth()->id() != null)
                            <input type="hidden" name="UserID" value="{{ auth()->id() }}">
                            @endif
                            <p class="text-white text-l mb-3">€{{ number_format($pizza->pizzaPrijs, 2) }}<br><span
                                    class="text-xs"> *Prijs verschilt van grootte</span></p>
                            <div class="flex justify-between items-center">

                                <select id="formaat" name="FormaatID" class="select rounded lg:mr-12 mr-0 w-24 py-1 px-1 flex">
                                    @foreach ($groottelijst as $grootte)

                                    <option value="<?php echo $grootte->id; ?>" <?php if ($grootte->id == '2'): ?>
                                        selected="selected" <?php endif; ?>>
                                        {{ strtoupper($grootte->grootte) }}</option>
                                    @endforeach
                                </select>
                                <input type="submit"
                                    class="bg-[#72C35C] text-white lg:px-4 lg:py-2 text-xl rounded-md hover:bg-[#61A84E] font-koulen px-2 py-1"
                                    value="Bestel Nu">
                            </div>
                    </div>
                </div>
                </form>
                @endforeach
            </div>
        </div>
        <!-- Rechter kolom -->
        <div class="hidden sm:block"></div>
    </div>
    @include('pizzeria.footer')
</body>

</html>