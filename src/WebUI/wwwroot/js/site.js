
let pageNumber = 1;
let loadingMore = false;

const fixMasonry = () => {
	const images = document.querySelectorAll('section.masonry img');
	for (let i = 0; i < images.length; i++) {
		fixMasonryImage(images[i]);
	}
}
function fixMasonryImage(img) {
	
	if (!img) {
		img = this;
	}
	const figure = img.parentNode.parentNode;
	

	let isLandscape = img.naturalWidth > img.naturalHeight * 1.8;
	let isPortait = img.naturalHeight > img.naturalWidth * 1.5;
	if (isLandscape)
		figure.classList.add("landscape");
	if (isPortait)
		figure.classList.add("portrait");

}
const loadMoreItems = () => {
	if (loadingMore)
		return;
	loadingMore = true;
	const loadMoreButton = document.querySelector(".load-more a");
	const pageNumber = parseInt(loadMoreButton.getAttribute("data-page")) + 1;
	const xhr = new XMLHttpRequest();
	let url = location.href.split("?")[0];
	url = url.endsWith('/') ? url.slice(0, -1) : url;
	xhr.open('GET',
		url + '/LoadMore?pageNumber=' + pageNumber,
		true);

	xhr.onreadystatechange = function () {
		if (xhr.readyState === 4 && xhr.status === 200) {
			const data = xhr.responseText;

			if (data.length > 0) {
				const loadMore = document.querySelector(".load-more");
				loadMore.parentElement.removeChild(loadMore);

				const itemContainer = document.querySelector('section.masonry');
				itemContainer.innerHTML += data;

				const images = itemContainer.querySelectorAll('img');
				images.forEach(image => {
					image.onerror = function () {
						this.parentNode.parentNode.style.display = "none";
					}
					image.addEventListener('load', () => {
						fixMasonryImage(image);
					});
				});
				loadingMore = false;
			}
		}
	};

	xhr.send();
}

window.addEventListener('load', () => {
	// JavaScript to toggle the menu
	const menuButton = document.querySelector('header nav>button');
	const navbar = document.querySelector('body');
	menuButton.addEventListener('click', () => {
		navbar.classList.toggle('menu-open');
	});

	fixMasonry();

	const loadMore = document.querySelector('.load-more');
	loadMore.addEventListener('click', function (e) {
		e.preventDefault();
		loadMoreItems();
	});

	window.addEventListener('scroll', function () {
		if (window.innerHeight + window.scrollY >= document.body.offsetHeight - 100) {
			loadMoreItems();
		}
	});
});