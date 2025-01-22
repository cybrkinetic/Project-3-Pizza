<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Uw bestellingen</title>
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
            <h1 class="text-4xl sm:text-4xl xl:text-5xl text-[#483F3F] font-koulen">Uw Bestellingen</h1>

            <!-- Bestelling lijst -->
            <div class="grid grid-cols-1 gap-4 px-4">
                @foreach ($orderlijst as $order)
                @if($order->OrderStatus->status != "Geannuleerd")
                <div class="bg-[#666060] p-4 rounded-md shadow-lg">
                    <p class="text-white text-lg mb-2">
                        #{{ $loop->iteration }}. OrderID: {{ $order->id }} - {{ $order->OrderStatus->status }}
                    </p>
                    <div class="flex justify-between items-center">
                        <a href="{{ route('orderlist.show', $order->id) }}"
                            class="bg-[#72C35C] hover:bg-[#61A84E] text-white font-koulen text-xl rounded-lg px-2 py-1 sm:px-5 sm:py-2.5">
                            Check Status
                        </a>
                        <form class="inline-block" action="{{ route('orderlist.destroy', $order->id) }}" method="POST">
                            @csrf
                            @method('DELETE')
                            <button type="submit"
                                class="bg-[#E8C63F] hover:bg-red-500 text-[#483F3F] font-koulen text-xl rounded-lg px-2 py-1 sm:px-5 sm:py-2.5">
                                Annuleer
                            </button>
                        </form>
                    </div>
                </div>
                @endif
                @endforeach
            </div>
        </div>

        <!-- Rechter kolom -->
        <div class="hidden sm:block"></div>
    </div>

    @include('pizzeria.footer')
</body>

</html>