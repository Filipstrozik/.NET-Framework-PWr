// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var canvases = document.querySelectorAll("canvas");
canvases.forEach(canv => draw(canv));


function draw(canv) {
    const canvas = canv;
    const rect = canvas.getBoundingClientRect()

    canvas.width = rect.width;
    canvas.height = rect.height;
    var isPresent = false;

    const ctx = canvas.getContext('2d')


    function drawLine(ctx, line) {
        const {
            start,
            end,
            lineWidth = 5,
            lineCap = 'round',
            strokeStyle = 'lightblue',
        } = line

        ctx.beginPath()
        ctx.moveTo(start.x, start.y)
        ctx.lineTo(end.x, end.y)
        ctx.lineWidth = lineWidth
        ctx.lineCap = lineCap
        ctx.strokeStyle = strokeStyle
        ctx.stroke()

    }

    function drawCircle(ctx, position) {
        ctx.beginPath()
        ctx.arc(position.x, position.y, 20, 0, 2 * Math.PI)
        ctx.strokeStyle = 'red'
        ctx.stroke()
    }

    // document.addEventListener('mousedown', function(e) {
    // isPressed = true
    // mouseDownPos = {
    //     x: e.clientX - canvas.offsetLeft,
    //     y: e.clientY - canvas.offsetTop
    // }

    // const line = {
    //     start: mouseDownPos,
    //     end: mouseDownPos,
    // }

    // drawLine(ctx, line)
    // })

/*
    document.addEventListener('mousemove', function (e) {
        var rect = canvas.getBoundingClientRect()

        if (isPresent) {
            let currentPos = {
                x: (e.clientX - rect.left),// / (rect.right - rect.left) * canvas.width,
                y: (e.clientY - rect.top),// / (rect.bottom - rect.top) * canvas.height
            }

            let lineLeftTop = {
                start: {
                    x: 0,
                    y: 0
                },
                end: currentPos
            }

            let lineRightTop = {
                start: {
                    x: canvas.width,
                    y: 0
                },
                end: currentPos
            }


            let lineLeftBottom = {
                start: {
                    x: 0,
                    y: canvas.height
                },
                end: currentPos
            }


            let lineRightBottom = {
                start: {
                    x: canvas.width,
                    y: canvas.height
                },
                end: currentPos
            }

            ctx.clearRect(0, 0, canvas.width, canvas.height)
            drawLine(ctx, lineLeftTop)
            drawLine(ctx, lineRightTop)
            drawLine(ctx, lineLeftBottom)
            drawLine(ctx, lineRightBottom)

        }
        // ctx.strokeRect(0,0,canvas.width, canvas.height)



    })
    */
    document.addEventListener('mousemove', function (e) {
        var rect = canvas.getBoundingClientRect()

        if (isPresent) {
            let currentPos = {
                x: (e.clientX - rect.left),// / (rect.right - rect.left) * canvas.width,
                y: (e.clientY - rect.top),// / (rect.bottom - rect.top) * canvas.height
            }

            let lineUp = {
                start: {
                    x: currentPos.x,
                    y: 0
                },
                end: currentPos
            }

            let lineRight = {
                start: {
                    x: canvas.width,
                    y: currentPos.y
                },
                end: currentPos
            }


            let lineLeft = {
                start: {
                    x: 0,
                    y: currentPos.y
                },
                end: currentPos
            }


            let lineDown = {
                start: {
                    x: currentPos.x,
                    y: canvas.height
                },
                end: currentPos
            }

            ctx.clearRect(0, 0, canvas.width, canvas.height)
            drawLine(ctx, lineUp)
            drawLine(ctx, lineRight)
            drawLine(ctx, lineLeft)
            drawLine(ctx, lineDown)
            drawCircle(ctx, currentPos)


        }
        // ctx.strokeRect(0,0,canvas.width, canvas.height)



    })



    canvas.addEventListener('mouseout', function (e) {
        isPresent = false;
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    })

    canvas.addEventListener('mouseover', function (e) {
        isPresent = true;
    })
}

