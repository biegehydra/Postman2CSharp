function deleteCookie(name) {
    var now = new Date();
    var time = now.getTime();
    time -= 3600 * 1000; // 1 hour in the past
    now.setTime(time);
    document.cookie = name + '=; expires=' + now.toUTCString() + '; path=/';
}

function setCookie(name, value) {
    var date = new Date();
    date.setDate(date.getDate() + 5);
    var json = encodeURIComponent(JSON.stringify(value));
    var cookie = name + "=" + json + "; expires=" + date.toUTCString() + "; path=/";
    document.cookie = cookie;
}

function getCookie(name) {
    var cookieArr = document.cookie.split(";");

    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");
        if (name == cookiePair[0].trim()) {
            return JSON.parse(decodeURIComponent(cookiePair[1]));
        }
    }
    return null;
}

function setLocalStorage(key, value) {
    var str = JSON.stringify(value);
    window.localStorage.setItem(key, str);
}

function getLocalStorage(key) {
    let value = window.localStorage.getItem(key);
    return JSON.parse(value);
}

function deleteLocalStorage(key) {
    window.localStorage.removeItem(key);
}

function clearLocalStorage() {
    window.localStorage.clear();
}

function scrollToSection(sectionId, scrollLength) {
    const element = document.getElementById(sectionId);
    const startPosition = window.pageYOffset;
    const targetRect = element.getBoundingClientRect();
    const viewportHeight = window.innerHeight;
    const targetPosition = startPosition + targetRect.top + targetRect.height / 2 - viewportHeight / 2;
    const distance = targetPosition - startPosition;
    const startTime = performance.now();

    function step(currentTime) {
        const elapsedTime = currentTime - startTime;
        const progress = Math.min(elapsedTime / scrollLength, 1);
        const easeInOutCubic = progress => (progress < 0.5 ? 4 * Math.pow(progress, 3) : 1 - Math.pow(-2 * progress + 2, 3) / 2);

        window.scrollTo(0, startPosition + distance * easeInOutCubic(progress));

        if (elapsedTime < scrollLength) {
            requestAnimationFrame(step);
        }
    }
    requestAnimationFrame(step);
}

function downloadFile(fileName, fileContent, fileExtension) {
    const content = new Blob([fileContent], { type: "text/plain" });

    const url = URL.createObjectURL(content);

    const link = document.createElement('a');
    link.href = url;
    link.download = fileName + fileExtension;
    link.click();

    setTimeout(function () {
        window.URL.revokeObjectURL(url);
        link.remove();
    }, 0);
}

function setAriaValueNow(elementId, value) {
    const element = document.getElementById(elementId);
    if (element) {
        element.setAttribute('aria-valuenow', value);
    }
}
function setElementStrokeDashOffset(elementId, value) {
    const parentElement = document.getElementById(elementId);
    if (parentElement) {
        const svgElement = parentElement.querySelector('svg');
        if (svgElement) {
            const circleElement = svgElement.querySelector('circle');
            if (circleElement) {
                circleElement.style.strokeDashoffset = value;
            }
        }
    }
}
function replaceElementContent (elementId, newContent) {
    const element = document.getElementById(elementId);
    if (element) {
        element.innerHTML = newContent;
    }
}

function loadScript (scriptPath) {
    if (loaded[scriptPath]) {
        return new this.Promise(function (resolve, reject) {
            resolve();
        });
    }

    return new Promise(function (resolve, reject) {
        var script = document.createElement("script");
        script.src = scriptPath;
        script.type = "text/javascript";

        loaded[scriptPath] = true;

        script.onload = function () {
            resolve(scriptPath);
        };

        script.onerror = function () {
            reject(scriptPath);
        }

        document["body"].appendChild(script);
    });
}
loaded = [];

function loadStyle(stylePath) {
    if (loaded[stylePath]) {
        return new this.Promise(function (resolve, reject) {
            resolve();
        });
    }

    return new Promise(function (resolve, reject) {
        var link = document.createElement("link");
        link.href = stylePath;
        link.type = "text/css";
        link.rel = "stylesheet";

        loaded[stylePath] = true;

        link.onload = function () {
            resolve(stylePath);
        };

        link.onerror = function () {
            reject(stylePath);
        }

        document["head"].appendChild(link);
    });
}

function setFaviconViewBox(elementId) {
    if (!elementId) {
        const svgElement = document.querySelector('.favicon-container .mud-icon-root.mud-icon-default.mud-svg-icon');
        svgElement.setAttribute('viewBox', '0 0 256 256');
    } else {
        const svgElement = document.getElementById(elementId);
        if (svgElement) {
            svgElement.setAttribute('viewBox', '0 0 256 256');
        }
    }
}

function applyMarkdown(elementId, markdownText) {
    var converter = new showdown.Converter(),
        html = converter.makeHtml(markdownText);

    var element = document.getElementById(elementId);
    element.innerHTML = html;
}

function emptyElement(elementId) {
    var element = document.getElementById(elementId);

    var viewportHeight = window.innerHeight || document.documentElement.clientHeight;

    var elementHeight = element.getBoundingClientRect().height;

    var scrollPosition = element.getBoundingClientRect().top + elementHeight / 2 - viewportHeight / 2;

    element.innerHTML = "";

    window.scrollTo(0, scrollPosition);
}
function splitVertical(id1, id2) {
    Split(['#' + id1, '#' + id2])
}

function getCurrentScrollPosition() {
    return window.scrollY || document.documentElement.scrollTop;
}

function scrollToSavedPosition(savedScrollPosition) {
    const observer = new MutationObserver(() => {
        if (document.readyState === 'complete') {
            requestAnimationFrame(() => {
                window.scrollTo({
                    top: savedScrollPosition,
                    behavior: 'auto'
                });
            });
            observer.disconnect();
        }
    });

    observer.observe(document, { childList: true, subtree: true });
}
