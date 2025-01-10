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
    <div class="grid w-full grid-cols-1 p-2 sm:grid-cols-[1fr,6fr,1fr] gap-4 min-h-screen">
        <!-- Left column -->
        <div class="hidden sm:block"></div>
        <!-- Center column -->
        <div class="flex flex-col justify-center space-y-8 h-[20rem] xl:h-[30rem] 2xl:h-[35rem]">
            <!-- Text header -->
            <h2 class="font-koulen text-3xl sm:text-4xl xl:text-5xl text-[#483F3F]">
                Zin in Pizza?
            </h2>
            <!-- Image -->
            <a href="/menu" class="block">
                <img src="img/PizzaAd.png" alt="Pizza Ad" class="rounded-xl max-w-full h-auto mx-auto items-center">
            </a>
        </div>
        <!-- Right column -->
        <div class="hidden sm:block"></div>
    </div>

</body>
</html>