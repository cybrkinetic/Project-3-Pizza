<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/css2?family=Koulen&display=swap" rel="stylesheet">

    <title>Document</title>
    <link rel="apple-touch-icon" sizes="180x180" href="img/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="img/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="img/favicon-16x16.png">
    <link rel="manifest" href="/site.webmanifest">

    @vite('resources/css/app.css')
</head>

<body>
    @include('pizzeria.header')

   <!-- Responsive grid -->
<div class="grid w-full grid-cols-1 p-4 sm:grid-cols-[1fr,6fr,1fr] md:grid-cols-[0.5fr,5fr,0.5fr] lg:grid-cols-[1.2fr,4fr,1.2fr] 2xl:grid-cols-[3.3fr,5fr,3.3fr] gap-4 md:gap-0 min-h-screen">
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
        <img src="img/PizzaAd.png" alt="Pizza Ad" class="rounded-xl sm:max-w-[50vh] sm:min-w-full md:max-w-[60vh] md:min-w-full lg:max-w-full h-auto object-contain">
      </a>
    </div>

    <!-- "Onze acties" gedeelte -->
    <div>
      <h2 class="font-koulen text-4xl xl:text-5xl text-[#483F3F] mb-4">
        Onze acties
      </h2>
      <div class="grid grid-cols-[repeat(auto-fill,_minmax(15rem,_1fr))] p-4 pt-0 sm:grid-cols-2 lg:grid-cols-4 gap-4">
        <!-- Acties -->
        <div class="aspect-square sm:w-full sm:h-60 md:w-full bg-gray-300 rounded-lg"></div>
        <div class="aspect-square sm:w-full sm:h-60 md:w-full bg-gray-300 rounded-lg"></div>
        <div class="aspect-square sm:w-full sm:h-60 md:w-full bg-gray-300 rounded-lg"></div>
        <div class="aspect-square sm:w-full sm:h-60 md:w-full bg-gray-300 rounded-lg"></div>
      </div>
    </div>
  </div>

  <!-- Rechter kolom -->
  <div class="hidden sm:block"></div>
</div>
@include('pizzeria.footer')
  
</body>
</html>