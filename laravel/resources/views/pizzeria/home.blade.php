<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/css2?family=Koulen&display=swap" rel="stylesheet">

    <title>Stonks Pizza</title>
    <link rel="apple-touch-icon" sizes="180x180" href="img/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="img/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="img/favicon-16x16.png">
    <link rel="manifest" href="/site.webmanifest">

    @vite('resources/css/app.css')
</head>

<body>
    @include('pizzeria.header')

    <!-- Responsive grid -->
    <div
        class="grid w-full grid-cols-1 p-4 sm:grid-cols-[0.9fr,6fr,0.9fr] md:grid-cols-[0.4fr,5fr,0.4fr] lg:grid-cols-[0.4fr,4fr,0.4fr] 2xl:grid-cols-[3.1fr,7fr,3.1fr] gap-4 md:gap-0 min-h-screen">
        <!-- Linkse kolom -->
        <div class="hidden sm:block"></div>

        <!-- Middel kolom -->
        <div class="flex flex-col space-y-8">
            <!-- "Zin in Pizza?" tekst -->
            <div>
                <h2 class="font-koulen text-4xl sm:text-4xl xl:text-5xl text-[#483F3F]">
                    Zin in Pizza?
                </h2>
            </div>

            <!-- Promo foto -->
            <div>
                <a href="/menu" class="block">
                    <img src="img/PizzaAd.png" alt="Pizza Ad"
                        class="rounded-xl sm:max-w-[50vh] sm:min-w-full md:max-w-[60vh] md:min-w-full lg:max-w-full h-auto object-contain">
                </a>
            </div>
            <div>
                <div>

                    <!-- Acties -->
                    <h2 class="font-koulen text-4xl xl:text-5xl text-[#483F3F] mb-4">
                        Onze acties
                    </h2>

                    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 p-4 pt-0">
                        <!-- Actie 1 -->
                        <div class="bg-[#666060] rounded-lg shadow-md flex flex-col h-[400px]">
                            <!-- Promo foto -->
                            <div class="h-2/3 bg-cover bg-center rounded-t-lg"
                                style="background-image: url('img/promotion1.jpg');"></div>
                            <!-- tekst -->
                            <div class="bg-[#483F3F] text-white p-4 flex flex-col justify-between flex-grow">
                                <h3 class="text-xl font-bold">Gratis pizza</h3>
                                <p>Je derde pizza gratis; exclusief voor onze members</p>
                                <button
                                    class="bg-[#72C35C] text-white text-2xl font-koulen px-4 py-2 rounded hover:bg-[#61A84E] mt-4"><a href="/menu">NAAR
                                    MENU</a></button>
                            </div>
                        </div>

                        <!-- Actie 2 -->
                        <div class="bg-[#666060] rounded-lg shadow-md flex flex-col h-[400px]">
                            <!-- Promo foto -->
                            <div class="h-2/3 bg-cover bg-center rounded-t-lg"
                                style="background-image: url('img/promotion2.jpg');"></div>
                            <!-- tekst -->
                            <div class="bg-[#483F3F] text-white p-4 flex flex-col justify-between flex-grow">
                                <h3 class="text-xl font-bold">Pizza voor 13,49</h3>
                                <p>Bij twee medium pizza's. Bij bezorgen.</p>
                                <button
                                    class="bg-[#72C35C] text-white text-2xl font-koulen px-4 py-2 rounded hover:bg-[#61A84E] mt-4"><a href="/menu">NAAR
                                    MENU</a></button>
                            </div>
                        </div>

                        <!-- Actie 3 -->
                        <div class="bg-[#666060] rounded-lg shadow-md flex flex-col h-[400px]">
                            <!-- Promo foto -->
                            <div class="h-2/3 bg-cover bg-center rounded-t-lg"
                                style="background-image: url('img/promotion3.jpg');"></div>
                            <!-- tekst -->
                            <div class="bg-[#483F3F] text-white p-4 flex flex-col justify-between flex-grow">
                                <h3 class="text-xl font-bold">Gratis funny meal</h3>
                                <p>Bij aankoop van twee medium pizza's</p>
                                <button
                                    class="bg-[#72C35C] text-white text-2xl font-koulen px-4 py-2 rounded hover:bg-[#61A84E] mt-4"><a href="/menu">NAAR
                                    MENU</a></button>
                            </div>
                        </div>

                        <!-- Actie 4 -->
                        <div class="bg-[#666060] rounded-lg shadow-md flex flex-col h-[400px]">
                            <!-- Promo foto -->
                            <div class="h-2/3 bg-cover bg-center rounded-t-lg"
                                style="background-image: url('img/promotion4.jpg');"></div>
                            <!-- tekst -->
                            <div class="bg-[#483F3F] text-white p-4 flex flex-col justify-between flex-grow">
                                <h3 class="text-xl font-bold">Hou ons in de gaten</h3>
                                <p>Er liggen nog meer acties op je te wachten...</p>
                                <button
                                    class="bg-[#72C35C] text-white text-2xl font-koulen px-4 py-2 rounded hover:bg-[#61A84E] mt-4"><a href="/menu">NAAR
                                    MENU</a></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>

        <!-- Rechter kolom -->
        <div class="hidden sm:block"></div>
    </div>
    @include('pizzeria.footer')

</body>

</html>