var player;

$('#youtube-modal').on('show.bs.modal', function (event) {
	var link = $(event.relatedTarget);
	var videoId = link.data('video-id');
	var videoTitle = link.data('video-title');
	var videoWidth = link.data('video-width');
	var videoHeight = link.data('video-height');

	var modal = $(this);
	modal.find('.modal-title').text(videoTitle);

	player = new YT.Player('player', {
		height: videoHeight,
		width: videoWidth,
		videoId: videoId
	});
});

$('#youtube-modal').on('hide.bs.modal', function (event) {
	$('#youtube-modal .modal-title').html('');
	$('#youtube-modal .modal-body').html('<div id="player"></div>');
});
